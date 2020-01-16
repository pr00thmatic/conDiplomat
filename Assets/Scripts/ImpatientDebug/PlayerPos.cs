using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerPos : MonoBehaviour {
  void Awake () {
    #if !UNITY_EDITOR
    transform.position = Vector3.Scale(transform.position,
                                       new Vector3(1,0,1));
    #endif
  }
}
