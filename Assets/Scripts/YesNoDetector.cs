using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class YesNoDetector : MonoBehaviour {
  public event System.Action<YesNoDetector, bool> onDetected;

  public Transform head;
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
    yes.Reset(head.transform);
    no.Reset(head.transform);
  }

  void Update () {
    Vector3 forward =
      head.transform.InverseTransformPoint(head.transform.position + yes.last.forward);
    Vector3 up =
      head.transform.InverseTransformPoint(head.transform.position + yes.last.up);
    yes.Update(tracker.velocity.x,
               Mathf.Abs(forward.x), Mathf.Abs(up.x), Mathf.Abs(forward.y));

    forward = head.transform.InverseTransformPoint(head.transform.position +
                                                   no.last.forward);
    up = head.transform.InverseTransformPoint(head.transform.position +
                                              no.last.up);
    no.Update(tracker.velocity.y,
              Mathf.Abs(forward.y), Mathf.Abs(up.x), Mathf.Abs(forward.x));
  }

  public void HandleShake (GenericShakeDetector caller) {
    if (onDetected != null) onDetected(this, caller == yes);
    print(caller == yes? "yes detected": "no detected");
  }
}
