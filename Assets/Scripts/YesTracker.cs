using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class YesTracker : MonoBehaviour {
  public YesNoReader debug;
  public int counter = 0;
  public AngularTracker angle;

  public RandomRange distanceAllowed;
  public RandomRange durationAllowed;

  public RandomRange deltaDistanceAllowed;
  public RandomRange deltaDurationAllowed;

  public bool isTracking = false;

  float _lastPolarityToggleTimestamp = 0;
  Vector3 _lastForward;
  float _lastPolarity;

  float _lastDuration;
  float _lastDistance;

  void Update () {
    if (Mathf.Sign(angle.lastVelocity.y) != Mathf.Sign(angle.velocity.y) &&
        angle.velocity.y > 15) {
      PolarityToggle();
      print("toggle! " + Time.time + " \ndistance " + _lastDistance +
            "\nduration " + _lastDuration +
            "\n polarity " + _lastPolarity);
    }
  }

  void PolarityToggle () {
    float duration = Time.time - _lastPolarityToggleTimestamp;
    float distance = transform.InverseTransformPoint(_lastForward).x;

    if (durationAllowed.Contains(duration) &&
        deltaDurationAllowed.Contains(Mathf.Abs(duration - _lastDuration)) &&
        distanceAllowed.Contains(Mathf.Abs(distance)) &&
        deltaDistanceAllowed.Contains(Mathf.Abs(_lastDistance - distance)) &&
        _lastPolarity != Mathf.Sign(distance)) {

      if (!isTracking) {
        isTracking = true;
        counter = 0;
      } else {
        counter++;
        debug.no.text = Time.time + "";
      }

    } else if (!distanceAllowed.Contains(Mathf.Abs(distance)) ||
               !durationAllowed.Contains(Mathf.Abs(duration))) {
      isTracking = false;
    }

    _lastPolarityToggleTimestamp = Time.time;
    _lastForward = transform.forward;
    _lastPolarity = Mathf.Sign(distance);
    _lastDuration = duration;
    _lastDistance = distance;

    debug.yes.text = counter + "";
  }
}
