using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EmotionManager : MonoBehaviour {
  public float transitionSpeed = 500;
  public SkinnedMeshRenderer skin;
  public Dictionary<Emotion, Coroutine> timers =
    new Dictionary<Emotion, Coroutine>();

  public void SetEmotion (Emotion emotion, bool value) {
    if (emotion == Emotion.Reset && value) {
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
    timers[emotion] = StartCoroutine(_SetEmotion(emotion, value));
  }

  IEnumerator _SetEmotion (Emotion emotion, bool value) {
    float target = value? 100: 0;
    float current = skin.GetBlendShapeWeight((int) emotion);

    while ((current < target && value)  || (current > target && !value)) {
      current += Time.deltaTime * transitionSpeed * (value? +1: -1);
      skin.SetBlendShapeWeight((int) emotion, current);
      yield return null;
    }
  }
}
