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

    public Vector3 velocity;
    public TextMesh text;

    int _cacheSize = 10;
    List<Vector3> _posCache = new List<Vector3>();

    bool _released = true;

    public void UpdateVelocity () {
        if (_posCache.Count >= _cacheSize) {
            _posCache.RemoveAt(0);
        }
        _posCache.Add(transform.position);

        velocity = Vector3.zero;
        for (int i=1; i<_posCache.Count; i++) {
            velocity = _posCache[i] - _posCache[i-1];
        }

        velocity = ((velocity / _posCache.Count) / Time.deltaTime) * 10;
        text.text = velocity + "";
    }

    void Update () {
        UpdateVelocity();

        pointer.gameObject.SetActive(!grabbed);

        if (!simulated) {
            float index = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller);
            float hand = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller);
            isGrabbing = index > grabStrengthTreshold || hand > grabStrengthTreshold;
        }

        if (_released && isGrabbing) {
            _released = false;
            Grab();
            GetComponentInChildren<Animator>().SetFloat("Flex", 1);
        }

        if (!isGrabbing && !_released) {
            _released = true;
            Release();
            GetComponentInChildren<Animator>().SetFloat("Flex", 0);
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
            grabbed.body.velocity = velocity;
            grabbed = null;
        }
    }
}
