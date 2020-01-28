using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActivationScript : MonoBehaviour, IScriptPiece {
  [SerializeField] NextTriggerer _triggerer;
  public NextTriggerer Triggerer { get => _triggerer; }
  public GameObject thing;
  public bool value;

  public void Execute () {
    thing.SetActive(value);
    Triggerer.TriggerFinish(this);
  }
}
