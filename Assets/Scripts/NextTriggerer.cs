using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class NextTriggerer {
  public event System.Action onFinish;
  public bool triggersNext = false;
  public bool waitsOnFinish = false;
  public float delay = 0;

  public void TriggerFinish (MonoBehaviour caller) {
    caller.StartCoroutine(_EventuallyFinish(caller));
  }

  IEnumerator _EventuallyFinish (MonoBehaviour caller) {
    yield return new WaitForSeconds(delay);
    if (onFinish != null) {
      onFinish();
    }
  }
}
