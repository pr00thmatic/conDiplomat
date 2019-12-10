using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Clothes : MonoBehaviour {
    public bool grabbed = false;
    public GameObject folded;
    public GameObject unfolded;
    public GameObject grabbable;

    void OnTriggerStay (Collider c) {
        SimulatedHand hand = c.GetComponentInParent<SimulatedHand>();
        if (!hand) return;

        if (!hand.isGrabbing) {
            unfolded.SetActive(false);
            folded.SetActive(true);
        } else if (!hand.grabbed) {
            unfolded.SetActive(false);
            folded.SetActive(false);

            hand.Grab(Instantiate(grabbable).GetComponent<Grabbable>());
        }
    }

    void OnTriggerExit (Collider c) {
        SimulatedHand hand = c.GetComponentInParent<SimulatedHand>();
        if (!hand) return;

        if (!grabbed) {
            unfolded.SetActive(true);
            folded.SetActive(false);
        }
    }
}
