using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class YesNoConsequenceActivation : MonoBehaviour {
  public YesNoQuestion action;
  public GameObject yes;
  public GameObject no;
  public GameObject silence;

  void OnEnable () {
    action.onResponse += HandleResponse;
  }

  void OnDisable () {
    action.onResponse -= HandleResponse;
  }

  public void HandleResponse (int count) {
    Debug.Log("handle response " + count, this);
    if (count > 0) {
      yes.SetActive(true);
    } else if (count < 0) {
      no.SetActive(true);
    } else {
      silence.SetActive(false);
    }
  }
}
