using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class YesNoQuestion : MonoBehaviour {
  public int bufferLength = 10;
  public Transform head;
  public Vector3 average;
  public ShakeDetector yes;
  public ShakeDetector no;
  public YesNoReader debug;

  Vector3 _lastAverage;
  Vector3[] _buffer;
  int _current = 0;
  Quaternion _lastRotation;

  void OnEnable () {
    yes.onShake += YesHandler;
    no.onShake += NoHandler;
  }

  void OnDisable () {
    yes.onShake -= YesHandler;
    no.onShake -= NoHandler;
  }

  void Start () {
    _buffer = new Vector3[bufferLength];
    for (int i=0; i<bufferLength; i++) {
      _buffer[i] = Vector3.zero;
    }
    _lastRotation = head.localRotation;
    average = Vector3.zero;
  }

  void Update () {
    average -= _buffer[_current] / (float) bufferLength;
    _current = (_current+1) % bufferLength;

    _buffer[_current] =
      (head.localRotation.eulerAngles - _lastRotation.eulerAngles) / Time.deltaTime;
    average += _buffer[_current] / (float) bufferLength;

    yes.Update(average.x);
    no.Update(average.y);


    _lastRotation = head.localRotation;
    _lastAverage = average;
  }

  public void YesHandler () {
    if (Mathf.Abs(average.y) < no.speedTolerance &&
        Mathf.Abs(average.z) < no.speedTolerance) {
      debug.Yes();
      print("yes " + Time.time);
    }
  }

  public void NoHandler () {
    if (Mathf.Abs(average.x) < yes.speedTolerance &&
        Mathf.Abs(average.z) < no.speedTolerance) {
      debug.No();
      print("no " + Time.time);
    }
  }

}
