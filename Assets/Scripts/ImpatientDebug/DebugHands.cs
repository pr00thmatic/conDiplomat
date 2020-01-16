using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DebugHands : MonoBehaviour {
  void Awake () {
    #if !UNITY_EDITOR
    gameObject.SetActive(false);
    #endif
  }
}
