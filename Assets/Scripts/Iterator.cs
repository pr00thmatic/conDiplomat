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
      float tmpElapsed = elapsed;
      iterable.onArtificialTrigger += ArtificialTriggerHandler;
      while (tmpElapsed < entry.milestone && !_artificiallyTriggered) {
        tmpElapsed += Time.deltaTime;
        yield return null;
      }

      if (_artificiallyTriggered) {
        _artificiallyTriggered = false;
      } else {
        elapsed = tmpElapsed;
      }
    }
  }

  public void ArtificialTriggerHandler () {
    _artificiallyTriggered = true;
  }
}
