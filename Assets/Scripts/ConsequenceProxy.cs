using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConsequenceProxy : MonoBehaviour, IScriptPiece {
  [SerializeField] NextTriggerer _triggerer; public NextTriggerer Triggerer { get => _triggerer; }

  public void Execute () {
    ChoisesConsequence[] consequences = GetComponentsInChildren<ChoisesConsequence>();
    bool found = false;

    foreach (ChoisesConsequence consequence in consequences) {
      if (consequence.canBeExecuted) {
        consequence.onFinish += HandleFinish;
        Util.Execute(consequence.gameObject);
        found = true;
      }
    }

    if (!found) {
      Util.Execute(GetComponentInChildren<ChoisesDefaultConsequence>().gameObject);
    }
  }

  public void HandleFinish () {
    Triggerer.TriggerFinish(this);
  }
}
