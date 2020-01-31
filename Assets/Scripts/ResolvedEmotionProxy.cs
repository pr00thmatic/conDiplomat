using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResolvedEmotionProxy : MonoBehaviour, IScriptPiece {
  [SerializeField] NextTriggerer _triggerer;
  public NextTriggerer Triggerer { get => _triggerer; }
  public ResolvedEmotion source;

  public void Execute () {
    EmotionConsequence[] consequences = GetComponentsInChildren<EmotionConsequence>();

    foreach (EmotionConsequence consequence in consequences) {
      if (consequence.expectedEmotion == source.CurrentEmotion) {
        Util.Execute(consequence.gameObject);
        return;
      }
    }

    Util.Execute(GetComponentInChildren<ChoisesDefaultConsequence>().gameObject);
  }
}
