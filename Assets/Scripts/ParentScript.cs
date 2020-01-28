using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParentScript : MonoBehaviour, IScriptPiece {
  [SerializeField] NextTriggerer _triggerer;
  public NextTriggerer Triggerer { get => _triggerer; }
  public Transform thing;
  public Transform anchor;

  public void Execute () {
    thing.parent = anchor;
    thing.localPosition = Vector3.zero;
    thing.localRotation = Quaternion.identity;
    Triggerer.TriggerFinish(this);
  }
}
