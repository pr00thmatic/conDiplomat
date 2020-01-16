using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimulatedHand : MonoBehaviour {
  public OVRInput.Controller controller;
  public bool simulated;
  public bool isGrabbing;
  public float grabStrengthTreshold = 0.1f;

  public Pointer pointer;
  public Grabbable grabbed;

  public Vector3 velocity;
  public TextMesh text;

  public Transform pivot;
  public float velocityMultiplier = 5;

  int _cacheSize = 5;
  Vector3[] _velocityCache;
  int _current = 0;

  bool _released = true;
  Vector3 _lastPos;

  void Awake () {
    _velocityCache = new Vector3[_cacheSize];
    _lastPos = transform.position;
  }

  public void UpdateVelocity () {
    _velocityCache[_current] = (_lastPos - transform.position) / Time.deltaTime;
    velocity += _velocityCache[_current] / (float) _cacheSize;
    velocity -= _velocityCache[(_current+1) % _cacheSize] / (float) _cacheSize;
    _current = (_current + 1) % _cacheSize;

    text.text = velocity + "";
    _lastPos = transform.position;
  }

  void Update () {
    UpdateVelocity();

    pointer.gameObject.SetActive(!grabbed);

    if (!simulated) {
      float index = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller);
      float hand = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller);
      isGrabbing = index > grabStrengthTreshold || hand > grabStrengthTreshold;
    }

    if (_released && isGrabbing) {
      _released = false;
      Grab();
      GetComponentInChildren<Animator>().SetFloat("Flex", 1);
    }

    if (!isGrabbing && !_released) {
      _released = true;
      Release();
      GetComponentInChildren<Animator>().SetFloat("Flex", 0);
    }
  }

  public void Grab () {
    if (pointer.target) {
      grabbed = pointer.target;
      pivot.position = grabbed.transform.position;
      pivot.rotation = grabbed.transform.rotation;
      grabbed.GetGrabbed(this);
    }
  }

  public void Release () {
    if (grabbed) {
      grabbed.GetReleased();
      grabbed.body.velocity = -velocity * velocityMultiplier;
      grabbed = null;
    }
  }
}
