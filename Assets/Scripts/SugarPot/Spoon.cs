using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SugarPot {
public class Spoon : MonoBehaviour {
  public Grabbable grabbable;
  public SpoonDetector detector;
  public GameObject colliders;
  public Rigidbody body;

  void OnEnable () {
    grabbable.onGrab += HandleGrab;
    grabbable.onRelease += HandleRelease;
  }

  void OnDisable () {
    grabbable.onGrab -= HandleGrab;
    grabbable.onRelease -= HandleRelease;
  }

  public void HandleRelease () {
    if (detector.pot) {
      colliders.SetActive(false);
      body.isKinematic = true;
      detector.pot.SetSpoon(this);
    } else {
      colliders.SetActive(true);
    }
  }

  public void HandleGrab () {
    transform.parent = null;
  }
}
}
