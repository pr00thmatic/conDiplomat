using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ThoughtBubble : MonoBehaviour {
  public Animator[] bubbles;
  public float waitBeforePop = 0.5f;

  public void Appear (float time) {
    StopAllCoroutines();
    StartCoroutine(_TemporizedAppear(time));
  }

  public void Disappear () {
    StopAllCoroutines();
    StartCoroutine(_SetVisibility(false));
  }

  IEnumerator _TemporizedAppear (float time) {
    yield return StartCoroutine(_SetVisibility(true));
    yield return new WaitForSeconds(time);
    StartCoroutine(_SetVisibility(false));
  }

  IEnumerator _SetVisibility (bool value) {
    foreach (Animator bubble in bubbles) {
      bubble.SetBool("is visible", value);
      yield return new WaitForSeconds(waitBeforePop);
    }
  }
}
