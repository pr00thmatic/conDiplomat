using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HumanParts {
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
      case HumanPartTarget.LookAside:
        found = target.GetComponentInChildren<AsideHumanPart>();
        break;
      case HumanPartTarget.LookAbove:
        found = target.GetComponentInChildren<Above>();
        break;
    }

    return found? found.transform: target;
  }
}
}
