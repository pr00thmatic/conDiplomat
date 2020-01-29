using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GiveQuestion : MonoBehaviour, IScriptPiece, IHaveAChoise {
  public NextTriggerer Triggerer { get => _triggerer; } [SerializeField] NextTriggerer _triggerer;
  public GameObject Choosen { get => _choise; }

  public float waitForAnswer = 0;
  public Grabbable requestedThing;

  public GameObject yes;
  public GameObject no;
  public GameObject thrownAway;

  public string scriptName;
  public float throwTreshold = 4; // m/s
  public TextMesh debug;

  GameObject _choise;
  bool _stop = false;

  public void Execute () {
    _choise = no;
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
      _choise = yes;
      _stop = true;
    }
  }

  void OnTriggerExit (Collider c) {
    Grabbable found = c.GetComponentInParent<Grabbable>();
    if (found == requestedThing) {
      _choise = no;
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
    _choise.transform.parent = transform.parent;
    _choise.name = scriptName;
    Triggerer.TriggerFinish(this);
  }

  void HandleThrownAway () {
    _choise = thrownAway;
  }

  public void HandleRelease () {
    debug.text = requestedThing.body.velocity.magnitude + "";
    if (requestedThing.body.velocity.magnitude > throwTreshold) {
      _stop = true;
      _choise = thrownAway;
    }
  }
}
