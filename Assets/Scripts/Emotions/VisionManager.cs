using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VisionManager : MonoBehaviour {
  public SmoothMover mover;
  public bool locked = false;

  public void SetVision (Transform thing) {
    mover.target = thing;
    locked = true;
  }

  public void ReleaseVision () {
    locked = false;
  }
}
