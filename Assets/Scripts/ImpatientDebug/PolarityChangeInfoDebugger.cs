using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PolarityChangeInfoDebugger : MonoBehaviour {
  public TextMesh display;
  public YesDetector target;

  void Update () {
    display.text = "polarity: " + target.last.polarity +
      "\ntimestamp: " + target.last.timestamp +
      "\nangle distance: " + target.last.angleDistance +
      "\nforward: " + target.last.forward +
      "\n\nraw times: " + target._rawTimes +
      "\nyes cointer: " + target.yesCounter +
      "\ntracker: (" +
      Mathf.Round(target.tracker.velocity.x) + ", " +
      Mathf.Round(target.tracker.velocity.y) + ", " +
      Mathf.Round(target.tracker.velocity.z) + ")" +
      "\nDEBUG: " + target.debug;
  }
}
