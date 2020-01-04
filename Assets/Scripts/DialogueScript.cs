using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogueScript : MonoBehaviour, IScriptPiece {
  public event System.Action onFinished;

  public EmotionManager emotions;
  public VisionManager target;
  public AudioSource voice;
  public AudioClip clip;
  public List<DialogueScriptEntry> script;
  public int nextOne = 0;
  public float elapsed = 0;

  void Reset () {
    emotions = transform.GetComponentInParent<EmotionManager>();
    voice = emotions.transform.GetComponentInChildren<AudioSource>();
    target = emotions.GetComponent<VisionManager>();
  }

  public void Execute () {
    StopAllCoroutines();
    StartCoroutine(_ExecuteScript());
    voice.clip = clip;
    voice.Play();
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

    if (onFinished != null) {
      onFinished();
    }
  }
}
