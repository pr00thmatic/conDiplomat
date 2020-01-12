using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriggerSomethingElse : MonoBehaviour, IScriptPiece {
  public NextTriggerer Triggerer { get => _triggerer; } [SerializeField] NextTriggerer _triggerer;

  public GameObject target;
  public float delay;

  void Reset () {
    VoiceScript voice = GetComponent<VoiceScript>();
    if (voice) {
      delay = voice.clip.length;
    }
  }

  public void Execute () {
    StartCoroutine(_EventuallyTrigger());
  }

  IEnumerator _EventuallyTrigger () {
    yield return new WaitForSeconds(delay);
    Util.Execute(target);
    Triggerer.TriggerFinish(this);
  }
}
