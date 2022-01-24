using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public Rigidbody rigidbody;
    public float speed;

    [Header("Shooting")]
    public float shootFireRate;

    [Header("Projectiles")]
    public float projectileSpeed;
    public float projectileLifetime;
    public int projectileMaxPoolSize;

    public GameObject projectilePrefab;
    private GameObject projectilePoolParent;
    private GameObject[] projectilePool;
    private Plane mousePlane;
    private int projectileIndex;

    void Awake()
    {
        projectilePoolParent = new GameObject("Projectile Pool");
        projectilePool = new GameObject[projectileMaxPoolSize];

        for (int i = 0; i < projectileMaxPoolSize; ++i)
        {
            projectilePool[i] = GameObject.Instantiate(projectilePrefab, projectilePoolParent.transform);
            projectilePool[i].SetActive(false);
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

        // Shooty logic
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = Vector3.zero;

            // Ensure we intercept mouse pos at the same height as the player
            mousePlane.SetNormalAndPosition(Vector3.up, new Vector3(0f, transform.position.y, 0f));

            // Shoot a ray to the plane and figure out what that world pos is (fast and efficient, no collider checks)
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            float distance;
            if (mousePlane.Raycast(mouseRay, out distance))
            {
                worldPos = mouseRay.GetPoint(distance);
                // Debug.DrawLine(Camera.main.ScreenToWorldPoint(Input.mousePosition),
                //         worldPos, Color.red, 5f);
            }

            // Set up projectile, advance projectile index
            GameObject projectileGO = projectilePool[projectileIndex];
            projectileGO.SetActive(true);
            Projectile projectile = projectileGO.GetComponent<Projectile>();
            projectile.Setup(this, transform.position, (worldPos - transform.position).normalized, projectileSpeed, projectileLifetime, 1f);

            if (++projectileIndex >= projectileMaxPoolSize)
            {
                projectileIndex -= projectileMaxPoolSize;
                }
        }

    }
}
