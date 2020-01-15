using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pointer : MonoBehaviour {
  public Grabbable target;
  public LineRenderer active;
  public LineRenderer inactive;
  public float maxDistance = 0.5f;
  public Grabbable triggerTarget;
  public Grabbable raycastTarget;

  public float _distance = Mathf.Infinity;

  public void ActivatePointer (float distance) {
    active.gameObject.SetActive(true);
    inactive.gameObject.SetActive(false);
    active.positionCount = 2;
    active.SetPositions(new Vector3[] {
        Vector3.zero, new Vector3(0,0, distance)
      });
  }

  public void DeactivatePointer () {
    inactive.positionCount = 2;
    inactive.SetPositions(new Vector3[] {
        Vector3.zero, new Vector3(0,0, maxDistance)
      });
    active.gameObject.SetActive(false);
    inactive.gameObject.SetActive(true);
  }

  public void SetTarget (Grabbable newTarget) {
    if (target && target != newTarget) {
      UnsetTarget();
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
  }

  void Update () {
    _distance = Mathf.Infinity;
    RaycastHit hit;

    if (Physics.Raycast(transform.position, transform.forward, out hit,
                        maxDistance, ~0, QueryTriggerInteraction.Ignore) &&
        hit.collider.GetComponentInParent<Grabbable>()) {

        raycastTarget = hit.collider.GetComponentInParent<Grabbable>();
    } else {
      raycastTarget = null;
    }

    if (triggerTarget && raycastTarget) {
      if (Vector3.Distance(triggerTarget.transform.position,
                           transform.position) < hit.distance) {
        SetTarget(triggerTarget);
      } else {
        SetTarget(raycastTarget);
      }
    } else if (triggerTarget || raycastTarget) {
      SetTarget(triggerTarget? triggerTarget: raycastTarget);
    } else {
      Debug.Log("unsetting", target);
      UnsetTarget();
    }

    if (target && raycastTarget == target) {
      ActivatePointer(hit.distance);
    } else {
      DeactivatePointer();
    }
  }

  void OnTriggerStay (Collider c) {
    Grabbable grabbable = c.GetComponentInParent<Grabbable>();
    if (!grabbable) return;
    float distance = Vector3.Distance(grabbable.transform.position,
                                      transform.position);
    if (distance >= _distance) return;

    _distance = distance;
    triggerTarget = grabbable;
  }

  void OnTriggerExit (Collider c) {
    Grabbable grabbable = c.GetComponentInParent<Grabbable>();
    if (!grabbable) return;
    if (triggerTarget == grabbable) {
      triggerTarget = null;
      _distance = Mathf.Infinity;
    }
  }
}
