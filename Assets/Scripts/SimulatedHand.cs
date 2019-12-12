using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimulatedHand : MonoBehaviour {
    public OVRInput.Controller controller;
    public bool simulated;
    public bool isGrabbing;
    public float grabStrengthTreshold = 0.1f;

    public Pointer pointer;

    void Update () {
        if (!simulated) {
            float index = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller);
            float hand = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger);
            isGrabbing = index > grabStrengthTreshold || hand > grabStrengthTreshold;
        }
    }
}
