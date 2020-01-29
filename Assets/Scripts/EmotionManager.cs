using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EmotionManager : MonoBehaviour {
  public bool IsBlockedByScript { get => blocker; }

  public float transitionSpeed = 500;
  public SkinnedMeshRenderer skin;
  public Dictionary<Emotion, Coroutine> timers =
    new Dictionary<Emotion, Coroutine>();
  public EmotionScript blocker;

  public void SetEmotion (Emotion emotion, bool value) {
    SetEmotion(emotion, value? 100: 0);
  }

  public void SetEmotion (Emotion emotion, float target) {
    // TODO: handle blocker
    // if (blocker) return;

    if (emotion == Emotion.Reset) {
      foreach (Emotion type in System.Enum.GetValues(typeof(Emotion))) {
        if (type != Emotion.Reset) {
          SetEmotion(type, false);
        }
      }
      return;
    }

    if (timers.ContainsKey(emotion) && timers[emotion] != null) {
      StopCoroutine(timers[emotion]);
    }
    timers[emotion] = StartCoroutine(_SetEmotion(emotion, target));
  }

  public void ResetEmotions () {
    SetEmotion(Emotion.Reset, 100);
  }

  IEnumerator _SetEmotion (Emotion emotion, float target) {
    float current = skin.GetBlendShapeWeight((int) emotion);
    float step = Mathf.Sign(target - current);

    while ((step < 0 && current > target) ||
           (step > 0 && current < target)) {
      current += Time.deltaTime * transitionSpeed * step;
      skin.SetBlendShapeWeight((int) emotion, current);
      yield return null;
    }

    skin.SetBlendShapeWeight((int) emotion, target);
  }
}
