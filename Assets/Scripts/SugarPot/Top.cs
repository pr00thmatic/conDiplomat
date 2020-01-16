using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SugarPot {
public class Top : MonoBehaviour {
  public Rigidbody body;
  public Pot pot;
  public float angleTollerance = 20;

  void Reset () {
    body = GetComponent<Rigidbody>();
  }

  void Update () {
    if (pot) {
      if (Vector3.Angle(pot.transform.up, Vector3.up) < angleTollerance) {
        transform.parent = null;
        body.isKinematic = false;
      }
    }
  }

  // void OnCollisionStay (Collision c) {
  //   pot = c.GetComponentInParent<Pot>();

  //   if (pot) {
  //     transform.parent = pot.transform;
  //     body.isKinematic = true;
  //   }
  // }

  // public bool CanBePartOf (Pot pot) {
    
  // }
}
}
