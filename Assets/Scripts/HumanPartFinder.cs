using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HumanPartFinder {
  public static Component Find (Transform target, HumanPartTarget part) {
    MonoBehaviour found = null;

    switch (part) {
      case HumanPartTarget.Head:
        found = target.GetComponentInChildren<HeadHumanPart>();
        break;
      case HumanPartTarget.RightHand:
        found = target.GetComponentInChildren<RightHandHumanPart>();
        break;
      case HumanPartTarget.LeftHand:
        found = target.GetComponentInChildren<LeftHandHumanPart>();
        break;
      case HumanPartTarget.LookFront:
        found = target.GetComponentInChildren<LookFront>();
        break;
    }

    return found? found.transform: target;
  }
}
