using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Closable : MonoBehaviour {
    public SimulatedHand hand;
    public Transform bone;
    public HingeJoint hinge;

    bool _markedOpen;

    public AudioSource speaker;

    public AudioClip[] yeeks;
    public AudioClip poing;
    public AudioClip click;
    public float yeekTolerance = 5;

    float _bufferTarget;
    float _bufferSpeed;
    bool _closeTriggered = false;

    public float speed;
    public float speedChange;

    void Update () {
	if (!hand) return;

	Vector3 distance = hand.transform.position - bone.transform.position;
	distance -= Vector3.Project(distance, bone.right);

	JointSpring spring = hinge.spring;
	spring.targetPosition =
	    Mathf.Lerp(100, 0, Vector3.SignedAngle(distance, bone.parent.forward,
						   bone.parent.right)/100f);
	hinge.spring = spring;

	UpdateSounds();

	_bufferSpeed = speed;
	_bufferTarget = spring.targetPosition;

	if (!hand.isGrabbing) {
	    hand = null;
	    Release();
	}
    }

    void UpdateSounds () {
	speed = (hinge.spring.targetPosition - _bufferTarget) / Time.deltaTime;
	speedChange = speed - _bufferSpeed;

	if (speedChange > yeekTolerance) {
	    speaker.PlayOneShot(yeeks[Random.Range(0, yeeks.Length)]);
	}

	if (hinge.spring.targetPosition == 100 && ! _closeTriggered) {
	    _closeTriggered = true;
	    speaker.PlayOneShot(click);
	} else if (hinge.spring.targetPosition != 100) {
	    _closeTriggered = false;
	}
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
	}
	hinge.spring = spring;
    }
}
