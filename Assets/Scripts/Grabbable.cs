using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grabbable : MonoBehaviour {
    public bool destroysOnDrop;
    public event System.Action<Grabbable> onDrop;

    public void Drop () {
        if (destroysOnDrop) {
            StartCoroutine(_DeferredDestroy());
        }

        if (onDrop != null) {
            onDrop(this);
        }
    }

    IEnumerator _DeferredDestroy () {
        yield return null;
        Destroy(gameObject);
    }
}
