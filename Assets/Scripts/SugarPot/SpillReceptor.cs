using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpillReceptor : MonoBehaviour {
  public event System.Action onSpillExceeded;

  public int limitReached = 0;
  [SerializeField]
  public float _spillReceived = 0;
  public float SpillReceived {
    get => _spillReceived + limitReached * spillTolerated;
  }
  public float spillTolerated = 3;

  public void IncreaseSpill (float amount) {
    _spillReceived += amount;

    if (_spillReceived >= spillTolerated) {
      limitReached += (int) Mathf.Floor(_spillReceived / spillTolerated);
      _spillReceived %= spillTolerated;
      if (onSpillExceeded != null) {
        onSpillExceeded();
      }
    }
  }
}
