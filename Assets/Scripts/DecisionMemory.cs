using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DecisionMemory : MonoBehaviour {
  public event System.Action onDecisionMade;

  public IHaveAChoise question;
  [SerializeField] GameObject _decision;
  public GameObject decision {
    get => _decision;
    set {
      decisionName = value.name;
      _decision = value;

      if (onDecisionMade != null) {
        onDecisionMade();
      }
    }
  }
  public string decisionName;
}
