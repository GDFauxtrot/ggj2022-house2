using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float yValue;

    [Range(0f, 20f)]
    public float interpolationValue;

    private static Transform currentTarget;

    void Awake()
    {
        transform.position = new Vector3(transform.position.x, yValue, transform.position.z);
    }

    void LateUpdate()
    {
        if (currentTarget)
        {
            // Figure out new position to lerp towards every frame
            Vector3 newPos = currentTarget.position;
            float yOffset = yValue - newPos.y;
            newPos += new Vector3(0f, yOffset, -yOffset);

            transform.position = Vector3.Lerp(transform.position, newPos, interpolationValue * Time.deltaTime);
        }
    }

    public static void SetCurrentTarget(Transform newTarget)
    {
        currentTarget = newTarget;
    }
}
