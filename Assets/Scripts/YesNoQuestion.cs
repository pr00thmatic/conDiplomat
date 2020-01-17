using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class YesNoQuestion : MonoBehaviour, IScriptPiece {
  public NextTriggerer Triggerer { get => _triggerer; } [SerializeField] NextTriggerer _triggerer;

  public string scriptName;
  public float waitForAnswer;
  public int counter;
  public YesNoDetector player;
  public GameObject yes;
  public GameObject no;
  public GameObject silence;

  public void Execute () {
    StartListening();
  }

  void Reset () {
    VoiceScript voice = GetComponent<VoiceScript>();
    if (voice) {
      waitForAnswer = voice.Length;
    }
  }

  void StartListening () {
    player.onDetected += AnswerHandler;
    StartCoroutine(_WaitForAnswer());
  }

  void StopListening () {
    player.onDetected -= AnswerHandler;
    GameObject target;
    if (counter > 0) {
      target = yes;
    } else if (counter < 0) {
      target = no;
    } else {
      target = silence;
    }
    target.SetActive(true);
    target.transform.parent = transform.parent;
    target.name = scriptName;
    Triggerer.TriggerFinish(this);
  }

  IEnumerator _WaitForAnswer () {
    yield return new WaitForSeconds(waitForAnswer);
    StopListening();
  }

  public void AnswerHandler (YesNoDetector caller, bool value) {
    counter = value? 1: -1; // i think this is better
    // it keeps the last answer
    // counter += value? 1: -1;
  }
}
