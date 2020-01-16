using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SugarPot {
public class Pot : MonoBehaviour {
  public Transform spoonPosition;
  public SugarSplatter sugar;
  public int loads;
  float _drainAmount;

  void Awake () {
    _drainAmount = 1/(float) loads;
  }

  public void Drain (Spoon spoon) {
    float sugarInSpoon = spoon.sugar.value * _drainAmount;
    sugar.value = Mathf.Min(1, sugarInSpoon + sugar.value);
    sugar.value = Mathf.Max(0, sugar.value - _drainAmount);
  }

  public void SetSpoon (Spoon spoon) {
    spoon.transform.parent = spoonPosition;
  }
}
}
