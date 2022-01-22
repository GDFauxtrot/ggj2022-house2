using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rigidbody;
    public float speed;

    void Start()
    {
        CameraController.SetCurrentTarget(transform);
    }

    void Update()
    {
        rigidbody.velocity = new Vector3(
                Input.GetAxis("Horizontal") * speed,
                rigidbody.velocity.y,
                Input.GetAxis("Vertical") * speed
        );
    }
}
