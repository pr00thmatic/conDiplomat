using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GiveQuestion : MonoBehaviour, IScriptPiece {
  public NextTriggerer Triggerer { get => _triggerer; } [SerializeField] NextTriggerer _triggerer;

  public float waitForAnswer = 0;
  public Grabbable requestedThing;
  public bool fulfilled = false;

  public GameObject yes;
  public GameObject no;

  public string scriptName;

  public void Execute () {
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
      fulfilled = true;
    }
  }

  void OnTriggerExit (Collider c) {
    Grabbable found = c.GetComponentInParent<Grabbable>();
    if (found == requestedThing) {
      fulfilled = false;
    }
  }

  IEnumerator _Listen () {
    yield return new WaitForSeconds(waitForAnswer);

    GameObject choise = fulfilled? yes: no;
    choise.transform.parent = transform.parent;
    choise.name = scriptName;
    Triggerer.TriggerFinish(this);
  }
}
