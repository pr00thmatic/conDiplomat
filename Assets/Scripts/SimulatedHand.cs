using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimulatedHand : MonoBehaviour {
    public Equipable focused;
    public Grabbable grabbed;

    public bool simulate = false;

    public GameObject grabIndicator;
    public bool isGrabbing;
    public OVRInput.Controller controller = OVRInput.Controller.RTouch;

    public float treshold = 0.1f;

    public bool _released = true;

    void Update () {
        if (!simulate) {
            isGrabbing =
                Mathf.Abs(OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller)) > treshold ||
                Mathf.Abs(OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller)) > treshold;
        }

        if (_released && isGrabbing) {
            _released = false;
            Grab();
        }

        if (!isGrabbing) {
            _released = true;
            if (grabbed) {
                Release();
            }
        }

        grabIndicator.SetActive(isGrabbing);
    }

    public void Grab () {
        if (!focused) return;

        focused.IsGrabbed = true;
        grabbed = focused.GetGrabbedCopy();
        grabbed.transform.SetParent(transform, false);
        focused = null;
    }

    public void Release () {
        if (grabbed) {
            grabbed.Drop();
            grabbed.transform.parent = null;
            grabbed = null;
        }
    }
}
