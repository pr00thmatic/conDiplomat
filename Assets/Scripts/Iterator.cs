using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Iterator {
  public IIterable iterable;
  public MonoBehaviour mono;

  public Iterator (IIterable iterable) {
    this.mono = iterable as MonoBehaviour;
    this.iterable = iterable;
  }

  public void Execute () {
    iterable.ResetToStart();
    mono.StopAllCoroutines();
    mono.StartCoroutine(_Execute());
  }

  IEnumerator _Execute () {
    ScriptEntry entry;
    float elapsed = 0;

    while (true) {
      entry = iterable.Step();
      if (entry == null) {
        iterable.TriggerFinish();
        break;
      }

      while (elapsed < entry.milestone) {
        elapsed += Time.deltaTime;
        yield return null;
      }
    }
  }
}
