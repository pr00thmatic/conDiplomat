using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Grabbable))]
[RequireComponent(typeof(BoxCollider))]
public class Reagent : MonoBehaviour, IGrabMediator {
  public Reagent GluedTo {
    get => !glue? null:
      (glue.connectedBody? glue.connectedBody.GetComponent<Reagent>(): null);
    set {
      if (!glue) {
        glue = gameObject.AddComponent<FixedJoint>();
      } else {
        GluedTo.onTop = null;
      }
      glue.connectedBody = value.GetComponent<Rigidbody>();
    }
  }
  public FixedJoint glue;
  public Reagent onTop;

  void OnTriggerStay (Collider c) {
    Reagent reagent = c.GetComponentInParent<Reagent>();

    if (reagent && !onTop) {
      PutOnTop(reagent);
    }
  }

  void Reset () {
    glue = gameObject.AddComponent<FixedJoint>();
    Transform detector = transform.Find("reagent detector");
    if (!detector) {
      detector = new GameObject("reagent detector").transform;
      detector.parent = transform;
      Util.ResetTransform(detector);
      BoxCollider collider = detector.gameObject.AddComponent<BoxCollider>();
      BoxCollider myCollider = GetComponent<BoxCollider>();
      collider.size = Vector3.Scale(new Vector3(0.9f, 0.9f, 1), myCollider.size);
      collider.center = myCollider.center + new Vector3(0,0, -0.1f);
      collider.isTrigger = true;
    }

    Transform anchor = transform.Find("next reagent anchor");
    if (!anchor) {
      anchor = new GameObject("next reagent anchor").transform;
      anchor.parent = transform;
      Util.ResetTransform(anchor);
    }
  }

  public void PutOnTop (Reagent reagent) {
    this.onTop = reagent;
    reagent.GluedTo = this;
  }

  public Reagent GlueOfGlues () {
    if (!GluedTo) return this;
    return GluedTo.GlueOfGlues();
  }

  public void HandleGrab (SimulatedHand hand) {
    GlueOfGlues().GetComponent<Grabbable>().ForceGrab(hand);
  }

  public void HandleRelease () {
    GlueOfGlues().GetComponent<Grabbable>().ForceRelease();
  }
}
