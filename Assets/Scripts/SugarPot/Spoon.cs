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
  public float potSpillPauseTime = 0.5f;

  Coroutine _spill;

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

  IEnumerator _EventuallySpill () {
    yield return new WaitForSeconds(potSpillPauseTime);
    sugar.canSpill = true;
  }

  public void HandlePotEntrance () {
    sugar.canSpill = false;

    if (grabbable.IsGrabbed == false) {
      body.isKinematic = true;
      detector.pot.SetSpoon(this);
    }
  }

  public void HandlePotExit () {
    _spill = StartCoroutine(_EventuallySpill());
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
    if (!detector.pot) return;
    detector.pot.Drain(this);
    sugar.Fill();
  }
}
}
