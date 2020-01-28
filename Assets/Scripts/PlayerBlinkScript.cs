using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerBlinkScript : MonoBehaviour, IScriptPiece {
  [SerializeField] NextTriggerer _triggerer;
  public NextTriggerer Triggerer { get => _triggerer; }
  public PlayerBlinker blink;
  public float duration = 0.1f;

  void Reset () {
    blink = GameObject.FindWithTag("Player").GetComponentInChildren<PlayerBlinker>();
  }

  public void Execute () {
    Triggerer.TriggerFinish(this);
    StartCoroutine(_Execute());
  }

  IEnumerator _Execute () {
    yield return new WaitForSeconds(Triggerer.delay);
    blink.Blink(true);
    yield return new WaitForSeconds(duration);
    blink.Blink(false);
  }
}
