using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ChoiseConditional {
  public bool IsMet { get => memory.decisionName == expected; }

  public string expected;
  public DecisionMemory memory;
}
