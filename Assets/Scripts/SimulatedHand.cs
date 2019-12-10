using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimulatedHand : MonoBehaviour {
    public bool simulate = false;

    public GameObject grabIndicator;
    public bool isGrabbing;
    public OVRInput.Controller controller = OVRInput.Controller.RTouch;

    public float treshold = 0.1f;

    public Grabbable grabbed;

    void Update () {
        if (!simulate) {
            isGrabbing =
                Mathf.Abs(OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller)) > treshold ||
                Mathf.Abs(OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller)) > treshold;
        }

        grabIndicator.SetActive(isGrabbing);
    }

    public void Grab (Grabbable grabbable) {
        grabbed = grabbable;
        grabbable.transform.SetParent(transform, false);
    }

    public void Release () {
        if (grabbed) {
            grabbed.Drop();
        }
    }
}
