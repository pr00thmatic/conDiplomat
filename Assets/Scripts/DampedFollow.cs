using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DampedFollow : MonoBehaviour {
  public Transform target;
  public Vector3 velocity;
  public float smoothTime = 0.2f;

  void Update () {
    transform.position =
      Vector3.SmoothDamp(transform.position, target.position,
                         ref velocity, smoothTime);
  }
}
