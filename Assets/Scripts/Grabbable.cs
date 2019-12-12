using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Outline))]
public class Grabbable : MonoBehaviour {
    public event System.Action onGrab;
    public static int releasedLayer = 9;

    public Rigidbody body;
    public Outline highlighter;
    public bool Highlighted { get => highlighter.enabled; }
    public Transform gripTransform;

    Transform _oldParent;
    Vector3 _originalPosition;

    void Awake () {
        _originalPosition = transform.position;
    }

    public void SetHighlight (bool value) {
        highlighter.enabled = value;
    }

    public void GetGrabbed (SimulatedHand hand) {
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider c in colliders) {
            c.enabled = false;
        }

        body.isKinematic = true;
        _oldParent = transform.parent;
        transform.SetParent(hand.transform, true);
        transform.localPosition = gripTransform.localPosition;
        transform.localRotation = gripTransform.localRotation;

        if (onGrab != null) {
            onGrab();
        }
    }

    public void GetReleased () {
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider c in colliders) {
            c.enabled = true;
        }

        body.isKinematic = false;
        transform.SetParent(_oldParent, true);
        StartCoroutine(_DeferredCollisionRestore());
    }

    IEnumerator _DeferredCollisionRestore () {
        int normal = gameObject.layer;
        Util.RecursiveSetLayer(releasedLayer, transform);

        yield return new WaitForSeconds(0.5f);

        Util.RecursiveSetLayer(normal, transform);
    }
}
