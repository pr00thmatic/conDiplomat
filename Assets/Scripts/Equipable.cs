using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Equipable : MonoBehaviour {
    public EquipableType type;
    public Luggage owner = null;

    void OnEnable () {
        GetComponent<Grabbable>().onGrab += GrabHandler;
    }

    void OnDisable () {
        GetComponent<Grabbable>().onGrab -= GrabHandler;
    }

    void OnTriggerEnter (Collider c) {
        Luggage luggage = c.GetComponentInParent<Luggage>();
        if (!luggage) return;
        if (luggage.items.Contains(this)) return;

        owner = luggage;
        owner.items.Add(this);
    }

    void OnTriggerExit (Collider c) {
        Luggage luggage = c.GetComponentInParent<Luggage>();
        if (!luggage) return;
        if (luggage != owner) return;

        GrabHandler();
    }

    public void GrabHandler () {
        if (owner) {
            owner.items.Remove(this);
            owner = null;
        }
    }
}
