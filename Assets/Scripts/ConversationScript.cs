using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConversationScript : MonoBehaviour, IScriptPiece {
  public event System.Action onFinished;
  public List<ConversationScriptEntry> dialogues;
  public int current = 0;

  public void Execute () {
    current = 0;
    HandleNext();
  }

  public void HandleNext () {
    if (current > 0) {
      dialogues[current-1].script.onFinished -= HandleNext;
    }
    StartCoroutine(_EventuallyExecuteNext());
  }

  IEnumerator _EventuallyExecuteNext () {
    if (current >= dialogues.Count) {
      if (onFinished != null) {
        onFinished();
      }
      yield break;
    }

    yield return new WaitForSeconds(dialogues[current].waitingTime);
    dialogues[current].script.Execute();
    dialogues[current].script.onFinished += HandleNext;
    current++;
  }
}
