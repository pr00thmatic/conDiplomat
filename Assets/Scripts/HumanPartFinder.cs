using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HumanPartFinder {
  public static Component Find (Transform target, HumanPartTarget part) {
    switch (part) {
      case HumanPartTarget.Head:
        return target.GetComponentInChildren<HeadHumanPart>();
      case HumanPartTarget.RightHand:
        return target.GetComponentInChildren<RightHandHumanPart>();
      case HumanPartTarget.LeftHand:
        return target.GetComponentInChildren<LeftHandHumanPart>();
    }

    return target;
  }
}
