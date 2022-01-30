using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAt : MonoBehaviour
{
    public TriggerEvents triggerEvent;
    public Transform target;
    public float yFactor = 1;
    [Range(0,1)]
    public float influence = 1;
    private float lerpFactor = 1;
    public float lerpTarget = 1;
    public float lerplerp = 0.4f;
    private Quaternion startRotation;
    public bool auto = true;

    private void Start() {
        startRotation = transform.localRotation;
    }
    public void resetLocalRotation() {
        transform.localRotation = Quaternion.Slerp(transform.localRotation, startRotation, 1-lerpFactor);
    }
    
    private void Update() {
        lerpFactor += (lerpTarget-lerpFactor) * lerplerp;
        if(auto&&target) {
            if(lerpTarget>0)PointAtTarget();
            else resetLocalRotation();
        }
    }

    public void SetPointerTargetFromTriggerEvent() {
        target = triggerEvent.collidingWith.transform;
    }

    public void PointAtTarget() {
    // Debug.Log(collidingWith);
    // transform.LookAt(collidingWith.transform);
    Vector3 diff = target.position - transform.position;
    diff.y *= yFactor;
    Quaternion rot = Quaternion.LookRotation(diff);
    transform.rotation = Quaternion.Slerp(transform.rotation, rot, lerpFactor*influence);
    }
    public void SetLerpLerpTarget(float lerplerpTarget) {
        lerpTarget = lerplerpTarget;
    }
    public void StartTarget() {
        lerpTarget = 1;
    }
    public void StopTarget() {
        lerpTarget = 0;
    }
}
