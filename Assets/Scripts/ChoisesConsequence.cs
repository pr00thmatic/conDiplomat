using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChoisesConsequence : MonoBehaviour {
  public event System.Action onFinish;

  public List<ChoiseConditional> conditions;
  public GameObject script;
  public bool canBeExecuted = false;

  void OnEnable () {
    foreach (ChoiseConditional condition in conditions) {
      condition.memory.onDecisionMade += HandleCondition;
    }
  }

  void OnDisable () {
    foreach (ChoiseConditional condition in conditions) {
      condition.memory.onDecisionMade -= HandleCondition;
    }
  }

  public void HandleCondition () {
    foreach (ChoiseConditional condition in conditions) {
      if (!condition.IsMet) {
        return;
      }
    }

    UnleashConsequence();
  }

  public void UnleashConsequence () {
    canBeExecuted = true;
  }

  public void Execute () {
    IScriptPiece[] pieces = Util.Execute(script);

    foreach (IScriptPiece piece in pieces) {
      piece.Triggerer.onFinish += HandleFinish;
    }
  }

  public void HandleFinish () {
    if (onFinish != null) {
      onFinish();
    }
  }
}
