using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Luggage : MonoBehaviour {
  public List<Equipable> items = new List<Equipable>();
  public TextMesh text;
  public Transform forceDirection;
  public float rejectionForceMultiplier = 5;
  public Closable closable;
  public ThoughtBubble bubble;

  void OnEnable () {
    closable.onClose += HandleClose;
  }

  void OnDisable () {
    closable.onClose -= HandleClose;
  }

  void Update () {
    text.text = "";

    foreach (Equipable equiped in items) {
      text.text += equiped.name + "\n";
    }
  }

  public void Equip (Equipable thing) {
    if (thing.GetComponent<Unique>()) {
      foreach (Equipable equiped in items) {
        if (equiped.type == thing.type) {
          ThrowAway(equiped);
          break;
        }
      }
    }

    items.Add(thing);
  }

  public void ThrowAway (Equipable thing) {
    thing.throwedUp = true;
    items.Remove(thing);
    // so the stuff on top of it won't get exploded!!
    thing.GetComponent<Unique>().TurnOffCollidersForABit();

    Vector3 direction = (forceDirection.transform.position -
                         thing.transform.position);

    thing.GetComponent<Rigidbody>()
      .AddForce(direction * rejectionForceMultiplier, ForceMode.Impulse);
  }

  public void HandleClose () {
    Equipable ticket = null;

    foreach (Equipable item in items) {
      if (item.type == EquipableType.Ticket) {
        ticket = item;
      }
    }

    if (ticket == null) {
      JointSpring spring = closable.hinge.spring;
      spring.targetPosition = 0;
      bubble.Appear(1);
      closable.ForceOpen();
    }
  }
}
