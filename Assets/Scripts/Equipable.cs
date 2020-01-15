using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Grabbable))]
[RequireComponent(typeof(Rigidbody))]
public class Equipable : MonoBehaviour {
  public EquipableType type;
  public Luggage owner = null;
  public bool throwedUp = false;
  public Rigidbody body;

  Grabbable _grabbable;

  void Reset () {
    body = GetComponent<Rigidbody>();
  }

  void OnCollisionStay (Collision c) {
    if (throwedUp && body.velocity.magnitude < 0.5f) {
      throwedUp = false;
    }
  }

  void OnEnable () {
    _grabbable = GetComponent<Grabbable>();
    _grabbable.onGrab += GrabHandler;
  }

  void OnDisable () {
    _grabbable.onGrab -= GrabHandler;
  }

  void OnTriggerStay (Collider c) {
    if (throwedUp || _grabbable.IsGrabbed || owner) return;

    Luggage luggage = c.GetComponentInParent<Luggage>();
    if (!luggage) return;
    if (luggage.items.Contains(this)) return;

    owner = luggage;
    owner.Equip(this);
  }

  void OnTriggerExit (Collider c) {
    if (throwedUp) return;

    Luggage luggage = c.GetComponentInParent<Luggage>();
    if (!luggage) return;
    if (luggage != owner) return;

    GrabHandler();
  }

  public void GrabHandler () {
    if (owner) {
      owner.items.Remove(this);
      owner = null;
    }
  }
}
