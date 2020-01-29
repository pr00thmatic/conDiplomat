using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ChoiseConditional {
  public bool IsMet { get => Question.Choosen == expected; }

  public GameObject expected;
  public IHaveAChoise Question { get => question as IHaveAChoise; }
  public MonoBehaviour question;
}
