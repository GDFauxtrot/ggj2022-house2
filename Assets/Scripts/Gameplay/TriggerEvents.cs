using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvents : MonoBehaviour
{
  public UnityEvent OnEnter;
  public UnityEvent OnExit;
  public UnityEvent OnStay;
  public Collider collidingWith;
  private Quaternion startRotation;

  private void Start() {
    startRotation = transform.localRotation;
  }
  public void ResetRotationLocal() {
    transform.localRotation = startRotation;
  }
  public void SaveRotationLocal() {
    startRotation = transform.localRotation;
  }

  private void OnTriggerEnter(Collider other) {
    collidingWith = other;
    OnEnter.Invoke();
  }

  private void OnTriggerExit(Collider other) {
    collidingWith = other;
    OnExit.Invoke();
  }

  private void OnTriggerStay(Collider other) {
    collidingWith = other;
    OnStay.Invoke();
  }

  public void PointAtCollidingWith() {
    // Debug.Log(collidingWith);
    // transform.LookAt(collidingWith.transform);
    transform.rotation = Quaternion.LookRotation(collidingWith.transform.position-transform.position);
  }
}