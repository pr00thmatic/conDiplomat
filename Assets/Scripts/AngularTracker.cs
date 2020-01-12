using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AngularTracker : MonoBehaviour {
  public Vector3 velocity;
  public Vector3 lastVelocity;

  public int bufferLength;
  Vector3[] _buffer;
  int _current = 0;
  Vector3 _lastRotation;

  void Start () {
    _buffer = new Vector3[bufferLength];
  }

  void Update () {
    lastVelocity = velocity;
    _buffer[_current] = (transform.rotation.eulerAngles - _lastRotation)/Time.deltaTime;
    velocity += _buffer[_current] / (float) bufferLength;
    velocity -= _buffer[(_current + bufferLength+1) % bufferLength] / (float) bufferLength;
    _current = (_current + 1) % bufferLength;

    _lastRotation = transform.rotation.eulerAngles;
  }
}
