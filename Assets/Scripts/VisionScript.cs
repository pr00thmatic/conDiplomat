using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VisionScript : MonoBehaviour, IScriptPiece, IIterable {
  public event System.Action onFinished;
  public VisionManager manager;
  public List<VisionScriptEntry> script;

  int _nextOne = 0;

  void Reset () {
    manager = transform.GetComponentInParent<VisionManager>();
  }

  public void Execute () {
    Iterator iterator = new Iterator(this);
    iterator.Execute();
  }

  public void ResetToStart () {
    _nextOne = 0;
  }

  public void TriggerFinish () {
    if (onFinished != null) {
      onFinished();
    }
  }

  public ScriptEntry Step () {
    if (_nextOne >= script.Count) return null;

    VisionScriptEntry current = script[_nextOne];
    manager.SetVision(HumanPartFinder.Find(current.target,
                                           current.optionalPart).transform);
    _nextOne++;
    return current as ScriptEntry;
  }
}
