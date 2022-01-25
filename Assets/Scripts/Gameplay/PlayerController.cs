using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public Rigidbody rigidbody;
    public float speed;

    [Header("Shooting")]
    [Min(0.01f)]
    public float shootFireRate = 1f;
    private float timeSinceLastShot;

    [Header("Projectiles")]
    public float projectileSpeed;
    public float projectileLifetime;
    [Min(1)]
    public int projectileMaxPoolSize;

    public GameObject projectilePrefab;
    private GameObject projectilePoolParent;
    private GameObject[] projectilePool;
    private int projectileIndex;

    [Header("Mouse")]
    public bool rotationFollowMouse;

    private Plane mousePlane;
    private Vector3 mouseWorldPos;


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

        mousePlane = new Plane(Vector3.up, 0f);
    }

    void Start()
    {
        CameraController.SetCurrentTarget(transform);
    }

    void Update()
    {
        // Super basic movement
        rigidbody.velocity = Vector3.ClampMagnitude(new Vector3(
                Input.GetAxisRaw("Horizontal") * speed,
                rigidbody.velocity.y,
                Input.GetAxisRaw("Vertical") * speed), speed);


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
        rigidbody.angularVelocity = Vector3.zero;

        // Shooty logic - if we have a pool and prefab, we can fire
        if (projectileMaxPoolSize > 0 && projectilePrefab)
        {
            float desiredShootTime = timeSinceLastShot + (1f/shootFireRate);
            if (Input.GetMouseButtonDown(0) || (Input.GetMouseButton(0) && desiredShootTime <= Time.timeSinceLevelLoad))
            {
                timeSinceLastShot = Time.timeSinceLevelLoad;
                if (!Input.GetMouseButtonDown(0))
                    timeSinceLastShot += (desiredShootTime - Time.timeSinceLevelLoad);

                // Set up projectile, advance projectile index
                GameObject projectileGO = projectilePool[projectileIndex];
                projectileGO.SetActive(true);
                Projectile projectile = projectileGO.GetComponent<Projectile>();
                projectile.Setup(this, transform.position, (mouseWorldPos - transform.position).normalized, projectileSpeed, projectileLifetime, 1f);

                if (++projectileIndex >= projectileMaxPoolSize)
                {
                    projectileIndex -= projectileMaxPoolSize;
                }
            }
        }

    }
}
