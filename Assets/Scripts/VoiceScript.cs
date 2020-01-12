using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VoiceScript : MonoBehaviour, IScriptPiece {
  // public event System.Action onFinished;
  public NextTriggerer Triggerer { get => _triggerer; } [SerializeField] NextTriggerer _triggerer;
  public float delay = 0;
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
    yield return new WaitForSeconds(delay);
    voice.clip = clip;
    voice.Play();
    while (voice.isPlaying) {
      yield return null;
    }
    voice.Stop();
    _triggerer.TriggerFinish(this);
  }
}
