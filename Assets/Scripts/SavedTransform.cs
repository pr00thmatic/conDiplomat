using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SavedTransform {
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;

    public void Save (Transform t) {
        position = t.position;
        rotation = t.rotation;
        scale = Util.GetScale(t);
    }

    public void Load (Transform t) {
        t.position = position;
        t.rotation = rotation;
        Util.SetScale(t, scale);
    }
}
