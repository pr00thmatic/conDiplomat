using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VoiceScript : MonoBehaviour, IScriptPiece {
  // public event System.Action onFinished;
  public NextTriggerer Triggerer { get => _triggerer; } [SerializeField] NextTriggerer _triggerer;
  public float delay = 0;
  public AudioSource voice;
  public List<AudioClip> clips;
  public float Length {
    get {
      float length = 0;
      foreach (AudioClip clip in clips) {
        length += clip.length;
      }

      return length;
    }
  }

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

    for (int i=0; i<clips.Count; i++) {
      voice.clip = clips[i];
      voice.Play();
      while (voice.isPlaying) {
        yield return null;
      }
      voice.Stop();
    }

    _triggerer.TriggerFinish(this);
  }
}
