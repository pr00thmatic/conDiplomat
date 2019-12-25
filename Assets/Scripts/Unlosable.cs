using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unlosable : MonoBehaviour {
    public Transform respawn;

    void Reset () {
        respawn = new GameObject("reset transform").transform;
        respawn.parent = transform;
        Util.ResetTransform(respawn);
    }

    void OnEnable () {
        respawn.transform.parent = null;
    }

    public void Respawn () {
        transform.position = respawn.position;
        transform.rotation = respawn.rotation;
        Util.ResetBody(GetComponentInChildren<Rigidbody>());
    }

    void OnTriggerEnter (Collider c) {
        if (c.GetComponent<UnreachableWall>()) {
            Respawn();
        }
    }
}
