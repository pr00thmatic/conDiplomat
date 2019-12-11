using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LuggageRecipient : MonoBehaviour {
    public Transform anchor;
    public Equipable equiped;
    public GameObject model;

    void OnTriggerEnter (Collider c) {
        SimulatedHand hand = c.GetComponentInParent<SimulatedHand>();
        if (!hand || !hand.grabbed) return;

        hand.grabbed.onDrop += HandleDrop;
    }

    void OnTriggerExit (Collider c) {
        SimulatedHand hand = c.GetComponentInParent<SimulatedHand>();
        if (!hand || !hand.grabbed) return;

        hand.grabbed.onDrop -= HandleDrop;
    }

    public void HandleDrop (Equipable owner) {
        if (model) {
            Destroy(model);
            equiped.SetInteractions(true);
        }

        equiped = owner;
        equiped.SetInteractions(false);
        model = equiped.GetEquipedCopy();
        model.transform.SetParent(anchor, false);
        model.transform.localPosition = Vector3.zero;
        model.transform.localScale = Vector3.one;
    }
}
