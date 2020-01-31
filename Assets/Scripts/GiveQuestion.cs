using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GiveQuestion : MonoBehaviour, IScriptPiece, IHaveAChoise {
  [SerializeField] DecisionMemory _memory;
  public DecisionMemory Memory { get => _memory? _memory: GetComponentInParent<DecisionMemory>(); }
  public NextTriggerer Triggerer { get => _triggerer; } [SerializeField] NextTriggerer _triggerer;
  public GameObject Choosen { get => Memory.decision; set => Memory.decision = value; }

  public float waitForAnswer = 0;
  public Grabbable requestedThing;

  public GameObject yes;
  public GameObject no;
  public GameObject thrownAway;

  public string scriptName;
  public float throwTreshold = 4; // m/s
  public TextMesh debug;

  bool _stop = false;

  public void Execute () {
    Memory.decision = no;
    StartCoroutine(_Listen());
  }

  void Reset () {
    VoiceScript voice = GetComponent<VoiceScript>();
    if (voice)
      waitForAnswer = voice.Length;
  }

  void OnTriggerStay (Collider c) {
    Grabbable found = c.GetComponentInParent<Grabbable>();
    if (found == requestedThing) {
      Choosen = yes;
      _stop = true;
    }
  }

  void OnTriggerExit (Collider c) {
    Grabbable found = c.GetComponentInParent<Grabbable>();
    if (found == requestedThing) {
      Choosen = no;
    }
  }

  IEnumerator _Listen () {
    requestedThing.onRelease += HandleRelease;
    VoiceScript voice = GetComponent<VoiceScript>();
    float elapsed = 0;

    while (elapsed < waitForAnswer || (_stop && elapsed < voice.Length)) {
      elapsed += Time.deltaTime;
      yield return null;
    }

    requestedThing.onRelease -= HandleRelease;
    Choosen.transform.parent = transform.parent;
    Choosen.name = scriptName;
    Triggerer.TriggerFinish(this);
  }

  void HandleThrownAway () {
    Choosen = thrownAway;
  }

  public void HandleRelease () {
    debug.text = requestedThing.body.velocity.magnitude + "";
    if (requestedThing.body.velocity.magnitude > throwTreshold) {
      _stop = true;
      Choosen = thrownAway;
    }
  }
}
