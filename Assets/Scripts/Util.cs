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
}
