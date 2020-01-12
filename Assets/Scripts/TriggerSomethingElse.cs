using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriggerSomethingElse : MonoBehaviour, IScriptPiece {
  public event System.Action onFinished;

  public GameObject target;
  public float delay;

  void Reset () {
    VoiceScript voice = GetComponent<VoiceScript>();
    if (voice) {
      delay = voice.clip.length;
    }
  }

  public void Execute () {
    print("eggsecutemua");
    StartCoroutine(_EventuallyTrigger());
  }

  IEnumerator _EventuallyTrigger () {
    yield return new WaitForSeconds(delay);
    Util.Execute(target);
    if (onFinished != null) onFinished();
  }
}
