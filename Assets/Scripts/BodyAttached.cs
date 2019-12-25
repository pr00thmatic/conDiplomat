using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BodyAttached : MonoBehaviour {
    public Rigidbody body;

    void OnEnable () {
        body.transform.parent = transform.parent;
        transform.parent = null;
    }
}
