using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Equipable : MonoBehaviour {
    public bool IsGrabbed {
        get => _isGrabbed;
        set {
            _isGrabbed = value;
            if (value) HandleGrab(); else HandleDrop();
        }
    }
    public GameObject equiped;
    public GameObject unfocused;
    public GameObject focused;
    public Grabbable grabbed;
    public bool allowInteractions = true;

    public bool _isGrabbed = false;

    public SimulatedHand ValidHand (Collider c) {
        SimulatedHand hand = c.GetComponentInParent<SimulatedHand>();
        if (!allowInteractions || IsGrabbed || !hand || hand.grabbed) return null;
        return hand;
    }

    // force focused target change
    void OnTriggerEnter (Collider c) {
        SimulatedHand hand = ValidHand(c);
        if (!hand) return;

        Equipable lastOne = hand.focused;
        hand.focused = this;
        UpdateHandStay(hand);

        if (lastOne) {
            lastOne.UpdateHandStay(hand);
        }
    }

    // let focused target free to use
    void OnTriggerExit (Collider c) {
        SimulatedHand hand = ValidHand(c);
        if (!hand) return;

        if (hand.focused == this) {
            hand.focused = null;
            unfocused.SetActive(true);
            focused.SetActive(false);
        }
    }

    // become focused if possible
    void OnTriggerStay (Collider c) {
        SimulatedHand hand = ValidHand(c);
        if (!hand) return;

        if (!hand.focused) {
            hand.focused = this;
        }

        UpdateHandStay(hand);
    }

    // update focused display when the hand is inside the trigger.
    public void UpdateHandStay (SimulatedHand hand) {
        if (IsGrabbed) return;

        if (hand.focused == this) {
            focused.SetActive(true);
            unfocused.SetActive(false);
        } else {
            focused.SetActive(false);
            unfocused.SetActive(true);
        }
    }

    void HandleGrab () {
        focused.SetActive(false);
        unfocused.SetActive(false);
    }

    void HandleDrop () {
        unfocused.SetActive(true);
        focused.SetActive(false);
    }

    public Grabbable GetGrabbedCopy () {
        grabbed.owner = this;
        Grabbable copy = Instantiate(grabbed);
        copy.gameObject.SetActive(true);

        return copy;
    }

    public void SetInteractions (bool value) {
        allowInteractions = value;

        unfocused.SetActive(value);
        if (!allowInteractions) {
            focused.SetActive(false);
        }
    }

    public GameObject GetEquipedCopy () {
        GameObject copy = Instantiate(equiped);
        copy.SetActive(true);

        return copy;
    }
}
