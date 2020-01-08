using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Conversation : MonoBehaviour, IScriptPiece, IIterable {
  public event System.Action onFinished;
  public float Delay { get => delay; } public float delay;
  public ConversationEntry[] script;
  public Transform[] actors;
  public string directory = "_SCRIPTS/";

  int _nextOne = 0;

  public void Execute () {
    Iterator iterator = new Iterator(this);
    iterator.Execute();
  }

  public void ResetToStart () {
    _nextOne = 0;
  }

  public void TriggerFinish () {
    if (onFinished != null) onFinished();
  }

  public ScriptEntry Step () {
    if (_nextOne >= script.Length) return null;
    ConversationEntry entry = script[_nextOne];

    foreach (Transform actor in actors) {
      Util.Execute(actor.Find(directory + "/" + entry.name));
    }

    _nextOne++;
    return entry;
  }
}
