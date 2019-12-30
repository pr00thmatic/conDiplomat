using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Closable : MonoBehaviour {
  public event System.Action onClose;

  public SimulatedHand hand;
  public Transform bone;
  public HingeJoint hinge;

  bool _markedOpen;

  public AudioSource speaker;

  public AudioClip[] yeeks;
  public AudioClip poing;
  public AudioClip click;
  public float yeekTolerance = 5;
  public float yeekCooldown = 1;

  float _bufferTarget;
  float _bufferSpeed;
  bool _closeTriggered = false;
  float _yeeked = 0;

  public float speed;
  public float speedChange;

  void Update () {
    if (!hand) return;

    UpdateHinge();
    UpdateSounds();
    UpdateHand();
    UpdateBuffers();
  }

  void UpdateSounds () {
    speed = (hinge.spring.targetPosition - _bufferTarget) / Time.deltaTime;
    speedChange = speed - _bufferSpeed;

    if ((Time.time - _yeeked) > yeekCooldown && speedChange > yeekTolerance) {
      _yeeked = Time.time;
      speaker.PlayOneShot(yeeks[Random.Range(0, yeeks.Length)]);
    }

    if (hinge.spring.targetPosition == 100 && ! _closeTriggered) {
      _closeTriggered = true;
      speaker.PlayOneShot(click);
    } else if (hinge.spring.targetPosition != 100) {
      _closeTriggered = false;
    }
  }

  void UpdateHand () {
    if (!hand.isGrabbing) {
      hand = null;
      Release();
    }
  }

  void UpdateBuffers () {
    if (speedChange < yeekTolerance ||
        Mathf.Sign(_bufferSpeed) != Mathf.Sign(speedChange)) {
      _yeeked = Time.time - yeekCooldown * 1.1f;
    }

    _bufferSpeed = speed;
    _bufferTarget = hinge.spring.targetPosition;
  }

  void UpdateHinge () {
    Vector3 distance = hand.transform.position - bone.transform.position;
    distance -= Vector3.Project(distance, bone.right);

    JointSpring spring = hinge.spring;
    spring.targetPosition =
      Mathf.Lerp(100, 0, Vector3.SignedAngle(distance, bone.parent.forward,
                                             bone.parent.right)/100f);
    hinge.spring = spring;
  }

  void OnTriggerStay (Collider c) {
    if (hand) return;

    SimulatedHand possibleHand = c.GetComponentInParent<SimulatedHand>();
    if (possibleHand && possibleHand.isGrabbing) {
      hand = possibleHand;
    }
  }

  public void Release () {
    JointSpring spring = hinge.spring;
    if (spring.targetPosition != 100) {
      spring.targetPosition = 0;
      speaker.PlayOneShot(poing);
    } else {
      if (onClose != null) {
        onClose();
      }
    }
    hinge.spring = spring;
  }
}
