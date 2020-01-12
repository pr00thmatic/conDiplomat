using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PolarityChangeInfoDebugger : MonoBehaviour {
  public YesNoDetector targetParent;
  public bool debugsYes = false;
  public TextMesh display;
  public GenericShakeDetector target;
  public int counter = 0;

  void Start () {
    target = debugsYes? targetParent.yes: targetParent.no;
    target.onShake += ShakeHandler;
  }

  void OnDisable () {
    target.onShake -= ShakeHandler;
  }

  void Update () {
    display.text = "polarity: " + target.last.polarity +
      "\ntimestamp: " + target.last.timestamp +
      "\nangle distance: " + target.last.angleDistance +
      "\nforward: " + target.last.forward +
      "\n\nraw times: " + target._rawTimes +
      "\nshake counter: " + counter +
      "\ntracker: (" +
      Mathf.Round(targetParent.tracker.velocity.x) + " - " +
      Mathf.Round(targetParent.tracker.velocity.y) + " - " +
      Mathf.Round(targetParent.tracker.velocity.z) + ")";
  }

  public void ShakeHandler (GenericShakeDetector caller) {
    counter++;
  }
}
