using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class YesDetector : MonoBehaviour {
  public float alienRotationEpsilon = 10;
  public float timeTolerance = 1;
  public float rotationEpsilon = 5;
  public AngularTracker tracker;

  public PolarityChangeInfo last;

  public int yesCounter = 0;

  public int _rawTimes = 0;

  public string debug;

  void Update () {
    if (Mathf.Abs(tracker.velocity.x) > rotationEpsilon &&
        Mathf.Sign(tracker.velocity.x) != Mathf.Sign(last.polarity) &&
        Mathf.Abs(tracker.velocity.y) < alienRotationEpsilon &&
        Mathf.Abs(tracker.velocity.z) < alienRotationEpsilon) {
      // polarity switch detected!

      float angleDistance = Mathf.Abs(transform.InverseTransformPoint(last.forward).y);
      if (timeTolerance > (Time.time - last.timestamp)) {  // &&
        // last.angleDistance * 0.2f <= angleDistance && angleDistance < 1.3f) {
        _rawTimes++;
        if (_rawTimes >= 3) {
          yesCounter++;
          _rawTimes = 0;
        }
      } else {
        debug = "nope, wrong timing " + (Time.time - last.timestamp);
        _rawTimes = 0;
      }

      last.polarity = Mathf.Sign(tracker.velocity.x);
      last.timestamp = Time.time;
      last.angleDistance = angleDistance;
      last.forward = transform.forward;
    }
  }
}
