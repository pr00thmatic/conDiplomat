using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MotionTest : MonoBehaviour {
  public float speed = 1;
  public Rigidbody body;

  void Start () {
    
  }
  
  void FixedUpdate () {
    body.MovePosition(transform.position  + new Vector3(Time.deltaTime * speed,0,0));
  }
}
