using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogueScript : MonoBehaviour {
  public EmotionManager emotions;
  public VisionManager target;
  public List<DialogueScriptEntry> script;
  public int nextOne = 0;
  public float elapsed = 0;

  public void Execute () {
    StopAllCoroutines();
    StartCoroutine(_ExecuteScript());
  }

  IEnumerator _ExecuteScript () {
    nextOne = 0;
    elapsed = 0;
    float milestone = 0;

    do {
      if (elapsed >= milestone) {
        emotions.SetEmotion(script[nextOne].emotion,
                            script[nextOne].milestone - milestone);
        milestone = script[nextOne].milestone;
        target.SetVision(script[nextOne].target);
        nextOne++;
      }

      elapsed += Time.deltaTime;
      yield return null;
    } while (nextOne < script.Count || elapsed < milestone);
  }
}
