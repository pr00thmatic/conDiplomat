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

    for (int i=0; i<levels.Count; i++) {
      if (levels[i].milestone >= target.limitReached) {
        // TODO: execute accordingly to the spill intensity
        // Util.Execute();
      }
    }
  }
}
