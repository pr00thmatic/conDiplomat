using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class YesNoDetector : MonoBehaviour {
  public event System.Action<YesNoDetector, bool> onDetected;

  public AngularTracker tracker;
  public GenericShakeDetector yes;
  public GenericShakeDetector no;

  void OnEnable () {
    yes.onShake += HandleShake;
    no.onShake += HandleShake;
  }

  void OnDisable () {
    yes.onShake -= HandleShake;
    no.onShake -= HandleShake;
  }

  void Reset () {
    tracker = GetComponent<AngularTracker>();
    yes.Reset(this.transform);
    no.Reset(this.transform);
  }

  void Update () {
    Vector3 forward =
      transform.InverseTransformPoint(transform.position + yes.last.forward);
    Vector3 up =
      transform.InverseTransformPoint(transform.position + yes.last.up);
    yes.Update(tracker.velocity.x,
               Mathf.Abs(forward.x), Mathf.Abs(up.x), Mathf.Abs(forward.y));

    forward = transform.InverseTransformPoint(transform.position + no.last.forward);
    up = transform.InverseTransformPoint(transform.position + no.last.up);
    no.Update(tracker.velocity.y,
              Mathf.Abs(forward.y), Mathf.Abs(up.x), Mathf.Abs(forward.x));
  }

  public void HandleShake (GenericShakeDetector caller) {
    if (onDetected != null) onDetected(this, caller == yes);
  }
}
