using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SugarPot {
public class Spoon : MonoBehaviour {
  public SugarSplatter sugar;
  public Grabbable grabbable;
  public SpoonDetector detector;
  public GameObject colliders;
  public Rigidbody body;

  void OnEnable () {
    grabbable.onGrab += HandleGrab;
    grabbable.onRelease += HandleRelease;
    detector.onSugarContact += HandleSugarContact;
  }

  void OnDisable () {
    grabbable.onGrab -= HandleGrab;
    grabbable.onRelease -= HandleRelease;
    detector.onSugarContact -= HandleSugarContact;
  }

  public void HandlePotEntrance () {
    if (grabbable.IsGrabbed == false) {
      body.isKinematic = true;
      detector.pot.SetSpoon(this);
    }
  }

  public void HandlePotExit () {
    if (grabbable.IsGrabbed == false) {
      body.isKinematic = false;
    }
  }

  public void HandleRelease () {
    if (detector.pot) {
      body.isKinematic = true;
      detector.pot.SetSpoon(this);
    }
  }

  public void HandleGrab () {
    transform.parent = null;
  }

  public void HandleSugarContact () {
    sugar.Fill();
  }
}
}
