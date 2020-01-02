using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BodyAttached : MonoBehaviour {
  public Rigidbody body;

  public void Detach () {
    BunHolder holder = transform.parent.GetComponent<BunHolder>();
    if (!holder) {
      holder = transform.parent.gameObject.AddComponent<BunHolder>();
    }
    holder.bun = transform;
    body.transform.parent = transform.parent;
    transform.parent = null;
  }

  void OnEnable () {
    Detach();
  }
}
