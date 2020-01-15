using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pointer : MonoBehaviour {
  public Grabbable target;
  public LineRenderer active;
  public LineRenderer inactive;
  public float maxDistance = 0.5f;

  public float _distance = Mathf.Infinity;

  public void ActivatePointer (float distance) {
    active.gameObject.SetActive(true);
    inactive.gameObject.SetActive(false);
    active.positionCount = 2;
    active.SetPositions(new Vector3[] {
        Vector3.zero, new Vector3(0,0, distance)
      });
  }

  public void SetTarget (Grabbable newTarget) {
    if (target && target != newTarget) {
      target.SetHighlight(false);
    }

    target = newTarget;

    if (!target.Highlighted) {
      target.SetHighlight(true);
    }
  }

  public void UnsetTarget () {
    if (target) {
      target.SetHighlight(false);
      target = null;
    }

    inactive.positionCount = 2;
    inactive.SetPositions(new Vector3[] {
        Vector3.zero, new Vector3(0,0, maxDistance)
      });
    active.gameObject.SetActive(false);
    inactive.gameObject.SetActive(true);
  }

  void Update () {
    _distance = Mathf.Infinity;
    // RaycastHit hit;
    // if (Physics.Raycast(transform.position, transform.forward, out hit,
    //                     maxDistance, ~0, QueryTriggerInteraction.Ignore) &&
    //     hit.collider.GetComponentInParent<Grabbable>() &&
    //     hit.distance <= _distance) {

    //   _distance = hit.distance;
    //   Grabbable grabbable = hit.collider.GetComponentInParent<Grabbable>();
    //   SetTarget(grabbable);
    //   ActivatePointer(_distance);
    // } else {
    //   if (Physics.Raycast(
    //   UnsetTarget();
    //   _distance = Mathf.Infinity;
    // }
  }

  void OnTriggerStay (Collider c) {
    Grabbable grabbable = c.GetComponentInParent<Grabbable>();
    if (!grabbable) return;
    float distance = Vector3.Distance(grabbable.transform.position,
                                      transform.position);
    if (distance >= _distance) return;

    _distance = distance;
    SetTarget(grabbable);
  }

  void OnTriggerExit (Collider c) {
    Grabbable grabbable = c.GetComponentInParent<Grabbable>();
    if (!grabbable) return;
    if (target == grabbable) {
      UnsetTarget();
      _distance = Mathf.Infinity;
    }
  }
}
