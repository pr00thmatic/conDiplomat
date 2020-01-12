using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IScriptPiece {
  public NextTriggerer Triggerer { get; }
  event System.Action onFinished;
  void Execute ();
}
