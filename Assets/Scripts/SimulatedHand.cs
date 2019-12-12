using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimulatedHand : MonoBehaviour {
    public OVRInput.Controller controller;
    public bool simulated;
    public bool isGrabbing;
    public float grabStrengthTreshold = 0.1f;

    public Pointer pointer;
    public Grabbable grabbed;

    bool _released = true;

    void Update () {
        pointer.gameObject.SetActive(!grabbed);

        if (!simulated) {
            float index = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller);
            float hand = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger);
            isGrabbing = index > grabStrengthTreshold || hand > grabStrengthTreshold;
        }

        if (_released && isGrabbing) {
            _released = false;
            Grab();
        }

        if (!isGrabbing && !_released) {
            _released = true;
            Release();
        }
    }

    public void Grab () {
        if (pointer.target) {
            grabbed = pointer.target;
            grabbed.GetGrabbed(this);
        }
    }

    public void Release () {
        if (grabbed) {
            grabbed.GetReleased();
            grabbed = null;
        }
    }
}
