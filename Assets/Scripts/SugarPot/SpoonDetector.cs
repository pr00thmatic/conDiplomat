using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SugarPot {
public class SpoonDetector : MonoBehaviour {
  public event System.Action onSugarContact;
  public Spoon target;
  public Pot pot;
  public string containedLayer;
  public string uncontainedLayer;

  void OnTriggerEnter (Collider c) {
    Sugar sugar = c.GetComponentInParent<Sugar>();
    if (sugar) {
      if (onSugarContact != null) onSugarContact();
      return;
    }
  }

  void OnTriggerStay (Collider c) {
    PotInterior interior = c.GetComponentInParent<PotInterior>();
    Pot foundPot = c.GetComponentInParent<Pot>();

    if (interior) {
      Util.RecursiveSetLayer(LayerMask.NameToLayer(containedLayer),
                             target.transform);
      pot = foundPot;
      target.HandlePotEntrance();
    }
  }

  void OnTriggerExit (Collider c) {
    PotInterior interior = c.GetComponentInParent<PotInterior>();
    Pot foundPot = c.GetComponentInParent<Pot>();
    if (interior) {
      Util.RecursiveSetLayer(LayerMask.NameToLayer(uncontainedLayer),
                             target.transform);
      if (foundPot == pot) {
        pot = null;
      }
      target.HandlePotExit();
    }
  }
}
}
