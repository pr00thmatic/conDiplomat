using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GenericShakeDetector {
  public event System.Action<GenericShakeDetector> onShake;

  public Transform target;
  public int timesRequired = 4;
  public float alienTorqueAngleEpsilon = 20;
  public float alienRotationAngleEpsilon = 10;
  public float timeTolerance = 1;
  public float rotationEpsilon = 5;
  public PolarityChangeInfo last;

  public int _rawTimes = 0;

  public void Reset (Transform target) {
    this.target = target;
  }

  public void Update (float velocity, float alienDistance, float alienTorque,
                      float angleDistance) {
    if (Mathf.Abs(velocity) > rotationEpsilon &&
        Mathf.Sign(velocity) != Mathf.Sign(last.polarity)) {
      // polarity switch detected!

      if (timeTolerance > (Time.time - last.timestamp) &&
          alienDistance < Mathf.Sin(alienRotationAngleEpsilon * Mathf.Deg2Rad) &&
          alienTorque < Mathf.Sin(alienTorqueAngleEpsilon * Mathf.Deg2Rad)) {  // &&
        // last.angleDistance * 0.2f <= angleDistance && angleDistance < 1.3f) {
        _rawTimes++;
        if (_rawTimes >= timesRequired) {
          if (onShake != null) onShake(this);
          _rawTimes = 0;
        }
      } else {
        _rawTimes = 0;
      }

      last.polarity = Mathf.Sign(velocity);
      last.timestamp = Time.time;
      last.angleDistance = angleDistance;
      last.forward = target.transform.forward;
      last.up = target.transform.up;
    }
  }
}
