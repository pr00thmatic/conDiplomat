using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpillScript : MonoBehaviour {
  public SpillReceptor[] spillTargets;
  public SpillReceptor trigger;
  public List<SpillLevel> levels;

  void OnEnable () {
    levels.Sort((SpillLevel a, SpillLevel b) => {
        return a.milestone.CompareTo(b.milestone);
      });

    foreach (SpillReceptor spillTarget in spillTargets) {
      spillTarget.onSpillExceeded += HandleSpill;
    }
  }

  void OnDisable () {
    foreach (SpillReceptor spillTarget in spillTargets) {
      spillTarget.onSpillExceeded -= HandleSpill;
    }
  }

  public void HandleSpill (SpillReceptor target) {
    this.trigger = target;

    for (int i=0; i<levels; i++) {
      if (levels[i].milestone >= target.limitReached) {
        Util.Execute();
      }
    }
  }
}
