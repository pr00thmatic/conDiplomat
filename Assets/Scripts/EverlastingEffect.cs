using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class EverlastingEffect : MonoBehaviour {
  public ResolvedEmotion[] affected;
  public SingleConsequence[] effects;
  public DecisionMemory decision;

  void OnEnable () {
    decision.onDecisionMade += HandleDecision;
  }

  void OnDisable () {
    decision.onDecisionMade -= HandleDecision;
  }

  public void HandleDecision () {
    SingleConsequence effect = null;

    foreach (SingleConsequence e in effects) {
      if (e.expectedDecision == decision.decisionName) {
        print("ding ding ding!! " + e.points);
        effect = e;
      }
    }

    Assert.IsTrue(effect != null, "effect is null!! O__O");

    foreach (ResolvedEmotion emotion in affected) {
      if (effect.overrides) {
        emotion.status = effect.points;
      } else {
        emotion.status = Mathf.Clamp(effect.points + emotion.status,
                                     0, emotion.available.Count-1);
      }
    }
  }
}
