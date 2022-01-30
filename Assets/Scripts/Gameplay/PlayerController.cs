using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static event Action PlayerDiedEvent;
    public static event Action<int> PlayerHealthChangedEvent;

    [Header("Movement")]
    public CharacterController characterController;
    public float speed;
    private float gravityValue = -9.81f;

    [Header("Shooting")]
    [Min(0.01f)]
    public float shootFireRate = 1f;
    private float timeSinceLastShot;

    [Header("Projectiles")]
    public float projectileSpeed;
    public float projectileLifetime;
    [Min(1)]
    public int projectileMaxPoolSize;
    public Transform projectileSource;

    public GameObject projectilePrefab;
    private GameObject projectilePoolParent;
    private GameObject[] projectilePool;
    private int projectileIndex;

    [Header("Mouse")]
    public bool rotationFollowMouse;

    private Plane mousePlane;
    private Vector3 mouseWorldPos;

    [Header("Animation")]
    public Animator animator;

    [Header("SoundEffects")]
    public RandomAudioPlayer shootAudio;

    [Header("Health")]
    [Min(1)]
    public int maxHealth;
    public static int health; // Since multiple players can exist
    private Vector3 velocity;
    public float pushPower = 1;


    
    void Awake()
    {
        projectilePoolParent = new GameObject("Projectile Pool");
        projectilePool = new GameObject[projectileMaxPoolSize];

        if (projectilePrefab)
        {
            for (int i = 0; i < projectileMaxPoolSize; ++i)
            {
                projectilePool[i] = GameObject.Instantiate(projectilePrefab, projectilePoolParent.transform);
                projectilePool[i].SetActive(false);
            }
        }
        else
        {
            Debug.LogWarning("No prefab assigned for projectiles! Shooting is disabled");
        }

        if (projectileMaxPoolSize == 0 && projectilePrefab)
        {
            Debug.LogWarning("Projectile pool size has somehow been assigned 0! Make sure to update this value in the inspector to enable shooting");
        }

        if (!projectileSource)
        {
            Debug.LogWarning("No projectile source assigned! Using this GameObject's own Transform");
            projectileSource = transform;
        }

        mousePlane = new Plane(Vector3.up, 0f);

        if (!animator)
        {
            animator = GetComponentInChildren<Animator>();
        }

        if (health < maxHealth) // In case of maxHealth-assigned inconsistency between instances
            health = maxHealth;

        PlayerDiedEvent += PlayerDied;
    }

    void Start()
    {
        CameraController.SetCurrentTarget(transform);
    }

    void OnDestroy()
    {
        PlayerDiedEvent -= PlayerDied;
    }

    void Update()
    {
        // Super basic movement
        Vector3 vel =  Vector3.ClampMagnitude(new Vector3(
                Input.GetAxisRaw("Horizontal") * speed,
                0,
                Input.GetAxisRaw("Vertical") * speed), speed);
        velocity.x = vel.x;
        velocity.z = vel.z;
        // velocity.y = characterController.isGrounded && characterController.velocity.y < 0 ? 0 : gravityValue; 
        velocity.y += gravityValue * Time.deltaTime;
        if(characterController.isGrounded && characterController.velocity.y < 0) velocity.y = 0;
        characterController.Move(velocity * Time.deltaTime);
        
        // Animate alongside movement
        if (animator)
        {
            bool moving = !Mathf.Approximately(Input.GetAxisRaw("Horizontal"), 0f) || !Mathf.Approximately(Input.GetAxisRaw("Vertical"), 0f);
            animator.SetBool("IsMoving", moving);
        }


        // Get mouse world pos - update intercept plane, shoot ray and get result
        mousePlane.SetNormalAndPosition(Vector3.up, new Vector3(0f, transform.position.y, 0f));
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        float distance;
        bool mousePosValid = mousePlane.Raycast(mouseRay, out distance);
        if (mousePosValid)
        {
            mouseWorldPos = mouseRay.GetPoint(distance);
        }

        if (rotationFollowMouse)
        {
            if (mousePosValid)
            {
                transform.LookAt(mouseWorldPos);
            }
        }
        else
        {
            transform.rotation = Quaternion.identity;
        }

        // Make sure it doesn't spin on its own
        // GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        // Shooty logic - if we have a pool and prefab, we can fire
        if (projectileMaxPoolSize > 0 && projectilePrefab)
        {
            // Cache operations (faster)
            bool mouse = Input.GetMouseButton(0);
            bool mouseDown = Input.GetMouseButtonDown(0);

            float desiredShootTime = timeSinceLastShot + (1f/shootFireRate);
            if (mouseDown || (mouse && desiredShootTime <= Time.timeSinceLevelLoad))
            {
                timeSinceLastShot = Time.timeSinceLevelLoad;
                if (!mouseDown)
                    timeSinceLastShot += (desiredShootTime - Time.timeSinceLevelLoad);

                // Figure out projectile direction normalized, discard Y (up/down)
                Vector3 projectileDir = (mouseWorldPos - projectileSource.position);
                projectileDir.y = 0f;
                projectileDir.Normalize();

                // Set up projectile, advance projectile index
                GameObject projectileGO = projectilePool[projectileIndex];
                projectileGO.SetActive(true);
                Projectile projectile = projectileGO.GetComponent<Projectile>();
                projectile.Setup(this, projectileSource.position, projectileDir, projectileSpeed, projectileLifetime, 1f);
                if(shootAudio)shootAudio.PlayRandomClip();

                if (++projectileIndex >= projectileMaxPoolSize)
                {
                    projectileIndex -= projectileMaxPoolSize;
                }

                // Set animation variables
                if (animator)
                {
                    animator.SetBool("HasShot", true);
                    if (mouseDown)
                    {
                        animator.SetBool("HasShotDown", true);
                    }
                }
            }

            // Reset animation variables
            if (!mouse)
                animator.SetBool("HasShot", false);
            if (!mouseDown)
                animator.SetBool("HasShotDown", false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("EnemyProjectile"))
        {
            // Recycle enemy projectile, reduce health
            ObjectPoolManager.Instance.RecycleIntoPool(ObjectPoolType.EnemyProjectile, other.gameObject);

            HurtPlayerOnce();
        }
    }

    // Public so that other things can trigger it as well (damaging floors, status effects, etc?)
    public void HurtPlayerOnce()
    {
        // Decrease health by 1, as advertised. Trigger events as well
        health -= 1;
        PlayerHealthChangedEvent(health);
        ProcessDead();
    }

    void ProcessDead()
    {
        if (health > 0)
            return;

        // Invoke player death. All interested objects can get notified and do logic (including this one)
        PlayerDiedEvent();
    }

    void PlayerDied()
    {
        // Player is the kill. Do a dead.
        gameObject.SetActive(false);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
            // no rigidbody
        if (body == null || body.isKinematic) { return; }
        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3) { return; }
        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        var pushDir = new Vector3 (velocity.x, 0, velocity.z);
        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.
        // Apply the push
        body.velocity = pushDir * pushPower;
    }
}
