using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SmoothMover : MonoBehaviour {
  public Transform target;
  public float speed = 4;

  void Update () {
    if (!target) return;
    transform.position = Vector3.MoveTowards(transform.position,
                                             target.transform.position,
                                             speed * Time.deltaTime);
  }
}
