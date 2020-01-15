using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Outline))]
[RequireComponent(typeof(Rigidbody))]
public class Grabbable : MonoBehaviour {
  public event System.Action onGrab;

  public SimulatedHand hand = null;
  public Rigidbody body;
  public Outline highlighter;
  public bool Highlighted { get => highlighter.enabled; }

  Transform _oldParent;
  Vector3 _originalPosition;
  Vector3 _offset;
  Quaternion _rotationOffset;

  void Reset () {
    body = GetComponent<Rigidbody>();
    highlighter = GetComponent<Outline>();
    highlighter.enabled = false;
  }

  void Awake () {
    _originalPosition = transform.position;
  }

  void Update () {
    if (hand) {
      body.MovePosition(hand.transform.position + _offset);
      body.MoveRotation(hand.transform.rotation * _rotationOffset);
    }
  }

  public void SetHighlight (bool value) {
    highlighter.enabled = value;
  }

  public void GetGrabbed (SimulatedHand hand) {
    if (this.hand) {
      this.hand.Release();
    }
    this.hand = hand;
    body.isKinematic = true;
    _oldParent = transform.parent;
    // transform.SetParent(hand.transform, true);
    _offset = transform.position - hand.transform.position;
    _rotationOffset = Quaternion.FromToRotation(hand.transform.forward,
                                                transform.forward);

    if (onGrab != null) {
      onGrab();
    }
  }

  public void GetReleased () {
    body.isKinematic = false;
    transform.SetParent(_oldParent, true);
    this.hand = null;
  }
}
