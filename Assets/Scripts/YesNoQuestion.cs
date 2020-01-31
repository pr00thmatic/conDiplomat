using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class YesNoQuestion : MonoBehaviour, IScriptPiece, IHaveAChoise {
  public NextTriggerer Triggerer { get => _triggerer; } [SerializeField] NextTriggerer _triggerer;
  [SerializeField] DecisionMemory _memory;
  public DecisionMemory Memory { get => _memory? _memory: GetComponentInParent<DecisionMemory>(); }
  public event System.Action<int> onResponse;
  public GameObject Choosen { get => Memory.decision; set => Memory.decision = value; }

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
    if (counter > 0) {
      Choosen = yes;
    } else if (counter < 0) {
      Choosen = no;
    } else {
      Choosen = silence;
    }
    Choosen.SetActive(true);
    Choosen.transform.parent = transform.parent;
    Choosen.name = scriptName;
    Triggerer.TriggerFinish(this);

    if (onResponse != null) {
      onResponse(counter);
    }
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
