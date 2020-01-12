using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Iterator {
  public IIterable iterable;
  public MonoBehaviour mono;

  bool _artificiallyTriggered = false;

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

    yield return new WaitForSeconds(iterable.Delay);

    while (true) {
      entry = iterable.Step();
      if (entry == null) {
        iterable.TriggerFinish();
        break;
      }

      // wait until
      if (entry.triggerer.waitsOnFinish) {
        entry.triggerer.onFinish += ArtificialTriggerHandler;
        while (!_artificiallyTriggered) {
          yield return null;
        }
        _artificiallyTriggered = false;
        yield return new WaitForSeconds(entry.triggerer.delay);
        elapsed += entry.triggerer.delay;
      } else {
        while (elapsed < entry.milestone) {
          elapsed += Time.deltaTime;
          yield return null;
        }
      }
    }
  }

  public void ArtificialTriggerHandler () {
    _artificiallyTriggered = true;
  }
}
