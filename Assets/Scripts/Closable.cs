using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Closable : MonoBehaviour {
    public SimulatedHand hand;
    public Transform bone;
    public HingeJoint hinge;

    void Update () {
        if (!hand) return;

        Vector3 distance = hand.transform.position - bone.transform.position;
        distance -= Vector3.Project(distance, bone.right);
        JointSpring spring = hinge.spring;
        spring.targetPosition =
            Mathf.Lerp(100, 0, Vector3.SignedAngle(distance, bone.parent.forward,
                                                   bone.parent.right)/100f);
        hinge.spring = spring;
        // Vector3 distance = hand.transform.position - bone.transform.position;
        // Quaternion newRotation =
        //     Quaternion.LookRotation(distance - Vector3.Project(distance, bone.right),
        //                             -Vector3.up);
        // if (Mathf.Abs(newRotation.eulerAngles.y) > 90) {
        //     newRotation =
        //         Quaternion.LookRotation(distance - Vector3.Project(distance, bone.right),
        //                                 Vector3.up);
        // }

        // bone.rotation = newRotation;

        if (!hand.isGrabbing) {
            hand = null;
            Release();
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
        bone.transform.forward = transform.parent.up;
    }
}
