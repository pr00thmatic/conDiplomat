using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Outline))]
[RequireComponent(typeof(Rigidbody))]
public class Grabbable : MonoBehaviour {
  public event System.Action onGrab;
  public event System.Action onRelease;
  public event System.Action onThrownAway;
  public bool IsGrabbed { get => hand != null; }

  public SimulatedHand hand = null;
  public Rigidbody body;
  public Outline highlighter;
  public bool Highlighted { get => highlighter.enabled; }

  Transform _oldParent;
  Vector3 _originalPosition;

  void Reset () {
    body = GetComponent<Rigidbody>();
    highlighter = GetComponent<Outline>();
    highlighter.OutlineWidth = 0.6f;
    // don't do this
    // highlighter.enabled = false;
  }

  void Awake () {
    _originalPosition = transform.position;
  }

  void Start () {
    StartCoroutine(_EventuallyUnhighlight());
  }

  void Update () {
    if (hand) {
      body.MovePosition(hand.pivot.position);
      body.MoveRotation(hand.pivot.rotation);
    }
  }

  void OnTriggerEnter (Collider c) {
    if (c.GetComponent<UnreachableWall>() && onThrownAway != null) {
      onThrownAway();
    }
  }

  public void SetHighlight (bool value) {
    highlighter.enabled = value;
  }

  public void ForceGrab (SimulatedHand hand, bool notReally = false) {
    if (this.hand) {
      this.hand.Release();
    }

    this.hand = hand;

    if (!notReally) {
      body.isKinematic = true;
      _oldParent = transform.parent;
      // transform.SetParent(hand.transform, true);
    }

    if (onGrab != null) {
      onGrab();
    }
  }

  public void GetGrabbed (SimulatedHand hand) {
    IGrabMediator grabMediator = GetComponent<IGrabMediator>();

    if (grabMediator != null) {
      grabMediator.HandleGrab(hand);
      ForceGrab(hand, true);
    } else {
      ForceGrab(hand);
    }
  }

  public void ForceRelease (bool notReally = false) {
    if (!notReally) {
      body.isKinematic = false;
      transform.SetParent(_oldParent, true);
    }

    this.hand = null;

    if (onRelease != null) {
      onRelease();
    }
  }

  public void GetReleased () {
    IGrabMediator grabMediator = GetComponent<IGrabMediator>();

    if (grabMediator != null) {
      grabMediator.HandleRelease();
      ForceRelease(true);
    } else {
      ForceRelease();
    }
  }

  IEnumerator _EventuallyUnhighlight () {
    yield return new WaitForSeconds(0.5f);
    highlighter.enabled = false;
  }
}
