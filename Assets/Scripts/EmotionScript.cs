using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EmotionScript : MonoBehaviour, IScriptPiece, IIterable {
  public event System.Action onFinished;
  public EmotionManager manager;
  public List<EmotionScriptEntry> script;

  int _nextOne = 0;

  void Reset () {
    manager = GetComponentInParent<EmotionManager>();
  }

  public void Execute () {
    Iterator iterator = new Iterator(this);
    iterator.Execute();
  }

  public void ResetToStart () {
    _nextOne = 0;
  }

  public void TriggerFinish () {
    if (script[script.Count-1].keep == false) {
      manager.SetEmotion(script[script.Count-1].emotion, false);
    }

    if (onFinished != null) {
      onFinished();
    }
  }

  public ScriptEntry Step () {
    if (_nextOne >= script.Count) return null;
    EmotionScriptEntry current = script[_nextOne];

    if (_nextOne > 0 && script[_nextOne-1].keep == false) {
      manager.SetEmotion(script[_nextOne-1].emotion, false);
    }
    manager.SetEmotion(current.emotion, true);

    _nextOne++;
    return current as ScriptEntry;
  }
}
