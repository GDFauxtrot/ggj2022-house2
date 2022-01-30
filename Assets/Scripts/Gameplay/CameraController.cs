using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private static Transform currentTarget;

    [Header("Camera Settings")]
    public new Camera camera;
    public float orthographicSize = 8f;

    [Header("Movement")]

    [Range(0, 90)]
    public float overheadAngle;

    [Range(0f, 20f)]
    public float interpolationValue;

    [Min(0f)]
    public float distance;

    public bool snapPosXToTarget;
    public bool lockRotToTarget;

    [Header("Screen Shake")]

    [Min(0f)]
    public float screenShakeAmount;
    [Min(0f)]
    public float screenShakeSpeed;
    [Range(0f, 1f)]
    public float screenShakeFalloff;

    private Vector3 screenShakeDisplacement;

    void Awake()
    {
        transform.position = new Vector3(transform.position.x, distance, transform.position.z);
        if (currentTarget)
        {
            transform.LookAt(currentTarget, Vector3.up);
        }

        if (!camera)
        {
            camera = GetComponent<Camera>();

            if (!camera)
            {
                camera = Camera.main;
            }
        }
    }

    void LateUpdate()
    {
        // Camera uses a different value than distance for orthographic size
        // because we don't want clipping while zoomed in close (screen shake can do this easily)
        camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, orthographicSize, interpolationValue * Time.deltaTime);

        // Undo screen shake from previous frame for calculations
        transform.position = transform.position - screenShakeDisplacement;

        if (currentTarget)
        {
            // Figure out X rotation in terms of transform displacement (YZ)
            Vector3 rot = new Vector3(0,
                    -Mathf.Cos(overheadAngle * Mathf.Deg2Rad),
                    Mathf.Sin(overheadAngle * Mathf.Deg2Rad));

            // Calculate and lerp to target pos + displacement
            Vector3 newPos = currentTarget.position - (rot * distance);

            Vector3 lerpPos = Vector3.Lerp(transform.position, newPos, interpolationValue * Time.deltaTime);

            transform.position = new Vector3(
                    snapPosXToTarget ? currentTarget.position.x : lerpPos.x,
                    lerpPos.y,
                    lockRotToTarget ? newPos.z : lerpPos.z);

            // Point camera at target
            transform.LookAt(currentTarget, Vector3.up);
        }

        // Apply screen shake if there is any
        if (screenShakeAmount > 0f)
        {
            // Caching, faster
            float timeSinceLoad = Time.timeSinceLevelLoad;

            // Traverse perlin noise texture in different amounts to get a cool variable screen shake effect
            Vector3 perlin = new Vector3(
                    Mathf.PerlinNoise(
                        timeSinceLoad * 0.95f * screenShakeSpeed,
                        timeSinceLoad * 1.025f * screenShakeSpeed) - 0.5f,
                    Mathf.PerlinNoise(
                        timeSinceLoad * 0.93f * screenShakeSpeed,
                        timeSinceLoad * 1.1f * screenShakeSpeed) - 0.5f,
                    Mathf.PerlinNoise(
                        timeSinceLoad * 1.22f * screenShakeSpeed,
                        timeSinceLoad * 1.23f * screenShakeSpeed) - 0.5f);

            screenShakeDisplacement = perlin * screenShakeAmount / 10f;
            transform.position = transform.position + screenShakeDisplacement;

            screenShakeAmount = Mathf.Lerp(screenShakeAmount, 0f, screenShakeFalloff * Time.deltaTime * 20);
            if (screenShakeAmount < 0.001f)
            {
                screenShakeAmount = 0f;
            }
        }
        else
        {
            screenShakeDisplacement = Vector3.zero;
        }
    }

    public static void SetCurrentTarget(Transform newTarget)
    {
        currentTarget = newTarget;
    }
}
