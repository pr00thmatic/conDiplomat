using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Equipable : MonoBehaviour {
    public EquipableType type;
    public Luggage owner = null;

    void OnTriggerEnter (Collider c) {
        Luggage luggage = c.GetComponentInParent<Luggage>();
        if (!luggage) return;
        if (owner.items.Contains(this)) return;

        owner = luggage;
        owner.items.Add(this);
    }

    void OnTriggerExit (Collider c) {
        Luggage luggage = c.GetComponentInParent<Luggage>();
        if (!luggage) return;
        if (luggage != owner) return;

        owner.items.Remove(this);
    }
}
