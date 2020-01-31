using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SugaryWater : MonoBehaviour {
  public SpillReceptor water;
  public bool blocked = false;

  void OnEnable () {
    water.onSpillExceeded += TriggerDialogue;
  }

  void OnDisable () {
    water.onSpillExceeded -= TriggerDialogue;
  }

  public void TriggerDialogue (SpillReceptor receptor) {
    if (blocked) return;
    GetComponent<IScriptPiece>().Execute();
    blocked = true;
  }
}
