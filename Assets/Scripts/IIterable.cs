using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IIterable {
  void ResetToStart ();
  ScriptEntry Step ();
  void TriggerFinish ();
}
