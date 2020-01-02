using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VisionScript : MonoBehaviour {
  public AudioClip debugClip;
  public AudioSource speaker;
  public VisionManager target;
  public List<VisionScriptEntry> script;
  public int nextOne = 0;
  public float elapsed = 0;
  public float nextMilestone = 0;

  void Update () {
    if (Input.GetKeyDown(KeyCode.Space)) {
      Activate();
    }
  }

  public void Activate () {
    speaker.PlayOneShot(debugClip);
    StopAllCoroutines();
    StartCoroutine(_ExecuteScript());
  }

  IEnumerator _ExecuteScript () {
    nextOne = 0;
    elapsed = 0;
    nextMilestone = 0;

    do {
      if (elapsed >= nextMilestone) {
        target.SetVision(script[nextOne].target);
        nextMilestone += script[nextOne].milestone;
        nextOne++;
      }

      elapsed += Time.deltaTime;
      yield return null;
    } while (nextOne < script.Count || elapsed < nextMilestone);
  }
}
