using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Util {
    public static void RecursiveSetLayer (int layer, Transform parent) {
        if (!parent) return;
        parent.gameObject.layer = layer;

        foreach (Transform child in parent) {
            RecursiveSetLayer(layer, child);
        }
    }

    public static void SetColliders (Transform owner, bool value) {
        Collider[] colliders = owner.GetComponentsInChildren<Collider>();

        foreach (Collider c in colliders) {
            c.enabled = value;
        }
    }
}
