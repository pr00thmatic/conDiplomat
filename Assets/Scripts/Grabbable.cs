using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grabbable : MonoBehaviour {
    public event System.Action<Equipable> onDrop;

    public bool destroysOnDrop;
    public Equipable owner;

    public void Drop () {
        if (destroysOnDrop) {
            StartCoroutine(_DeferredDestroy());
        }

        owner.IsGrabbed = false;

        if (onDrop != null) {
            onDrop(owner);
        }
    }

    IEnumerator _DeferredDestroy () {
        yield return null;
        Destroy(gameObject);
    }
}
