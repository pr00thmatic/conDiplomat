using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IIterable {
  event System.Action onArtificialTrigger;
  event System.Action onFinished;
  float Delay { get; }
  void ResetToStart ();
  void TriggerFinish ();
  ScriptEntry Step ();
}
