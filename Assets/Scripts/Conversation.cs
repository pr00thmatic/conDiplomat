using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Conversation : MonoBehaviour, IScriptPiece, IIterable {
  public NextTriggerer Triggerer { get => _triggerer; } [SerializeField] NextTriggerer _triggerer;
  public event System.Action onArtificialTrigger;
  public event System.Action onFinished;
  public float Delay { get => delay; } public float delay;
  public ConversationEntry[] script;
  public Transform[] actors;
  public string directory = "_SCRIPTS/";

  public int _nextOne = 0;

  public float finishDelay;
  public GameObject executeOnFinish;

  IScriptPiece[] _triggerers;

  public void Execute () {
    Iterator iterator = new Iterator(this);
    iterator.Execute();
  }

  public void ResetToStart () {
    _nextOne = 0;
  }

  public void TriggerFinish () {
    Triggerer.TriggerFinish(this);

    if (executeOnFinish) {
      StartCoroutine(_EventuallyExecute());
    }
  }

  public ScriptEntry Step () {
    if (_nextOne >= script.Length) return null;
    ConversationEntry entry = script[_nextOne];

    foreach (Transform actor in actors) {
      _triggerers = Util.Execute(actor.Find(directory + "/" + entry.name));
      foreach (IScriptPiece triggerer in _triggerers) {
        triggerer.Triggerer.onFinish += HandleTrigger;
      }
    }

    _nextOne++;
    return entry;
  }

  public void HandleTrigger () {
    foreach (IScriptPiece trigger in _triggerers) {
      trigger.Triggerer.onFinish -= HandleTrigger;
    }
    _triggerers = null;
    script[_nextOne-1].triggerer.TriggerFinish(this);
  }

  IEnumerator _EventuallyExecute () {
    yield return new WaitForSeconds(finishDelay);
    Util.Execute(executeOnFinish);
  }
}
