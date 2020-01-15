using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SugarPot {
public class SpoonDetector : MonoBehaviour {
  public event System.Action onSugarContact;
  public Pot pot;

  void OnTriggerEnter (Collider c) {
    Sugar sugar = c.GetComponentInParent<Sugar>();
    if (sugar) {
      if (onSugarContact != null) onSugarContact();
      return;
    }
  }

  void OnTriggerStay (Collider c) {
    Pot found = c.GetComponentInParent<Pot>();
    if (found) {
      pot = found;
    }
  }

  void OnTriggerExit (Collider c) {
    Pot found = c.GetComponentInParent<Pot>();
    if (this.pot == found) {
      pot = null;
    }
  }
}
}
