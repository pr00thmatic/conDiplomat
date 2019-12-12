using UnityEngine;
using UnityEngine.Animations;
using System.Collections;
using System.Collections.Generic;

public class AutoVolume : MonoBehaviour {
    public ParentConstraint source;

    void Reset () {
        source = GetComponent<ParentConstraint>();
        ConstraintSource fuck = source.GetSource(0);
        fuck.sourceTransform = transform.parent;
        source.SetSource(0, fuck);
    }
}
