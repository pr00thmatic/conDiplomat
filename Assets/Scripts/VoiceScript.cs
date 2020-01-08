using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VoiceScript : MonoBehaviour, IScriptPiece {
  public event System.Action onFinished;
  public AudioSource voice;
  public AudioClip clip;

  void Reset () {
    voice = transform.GetComponentInParent<EmotionManager>()
      .GetComponentInChildren<AudioSource>();
  }

  public void Execute () {
    StopAllCoroutines();
    StartCoroutine(_Execute());
  }

  IEnumerator _Execute () {
    voice.clip = clip;
    voice.Play();
    while (voice.isPlaying) {
      yield return null;
    }
    voice.Stop();
    if (onFinished != null) {
      onFinished();
    }
  }
}
