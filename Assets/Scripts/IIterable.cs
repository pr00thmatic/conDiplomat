using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IIterable {
  float Delay { get; }
  void ResetToStart ();
  void TriggerFinish ();
  ScriptEntry Step ();
}
