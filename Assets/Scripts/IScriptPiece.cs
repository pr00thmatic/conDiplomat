using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IScriptPiece {
  // event System.Action onFinished;
  NextTriggerer Triggerer { get; }
  void Execute ();
}
