using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoseScript : MonoBehaviour, IScriptPiece {
  [SerializeField] NextTriggerer _triggerer;
  public NextTriggerer Triggerer { get => _triggerer; }
  public Animator animator;
  public string trigger;

  void Reset () {
    animator = GetComponentInParent<Blinker>().GetComponentInChildren<Animator>();
  }

  public void Execute () {
    animator.SetTrigger(trigger);
    Triggerer.TriggerFinish(this);
  }
}
