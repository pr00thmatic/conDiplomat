using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rotator : MonoBehaviour {
  public float speed = 5;

  void Update () {
    transform.Rotate(Vector3.up * speed * Time.deltaTime);
  }
}
