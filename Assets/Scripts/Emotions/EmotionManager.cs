using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EmotionManager : MonoBehaviour {
  public float transitionDuration = 0.25f;
  public SkinnedMeshRenderer skin;
  public Dictionary<Emotion, EmotionTimer> timers =
    new Dictionary<Emotion, EmotionTimer>();

  void Update () {
    // uncomment to debug!
    // if (Input.GetKeyDown(KeyCode.Q)) {
    //   SetEmotion(Emotion.Mad, 1);
    // }

    // if (Input.GetKeyDown(KeyCode.W)) {
    //   SetEmotion(Emotion.Weird, 1);
    // }

    // if (Input.GetKeyDown(KeyCode.E)) {
    //   SetEmotion(Emotion.Happy, 1);
    // }

    // if (Input.GetKeyDown(KeyCode.R)) {
    //   SetEmotion(Emotion.Sad, 1);
    // }

    foreach (var item in timers) {
      if (item.Value.time > 0 && !item.Value.turnedOn) {
        TurnEmotion(item.Key, true);
      }

      if (item.Value.time <= 0 && !item.Value.turnedOff) {
        TurnEmotion(item.Key, false);
      }

      item.Value.time -= Time.deltaTime;
    }
  }

  public void SetEmotion (Emotion emotion, float duration) {
    EmotionTimer timer;

    if (timers.ContainsKey(emotion)) {
      timer = timers[emotion];
    } else {
      timer = new EmotionTimer();
      timers[emotion] = timer;
    }

    timer.time = duration;
  }

  void TurnEmotion (Emotion emotion, bool value) {
    timers[emotion].turnedOn = value;
    timers[emotion].turnedOff = !value;
    StartCoroutine(_Set(emotion, value? 100: 0));
  }

  IEnumerator _Set (Emotion emotion, float value) {
    float initial = skin.GetBlendShapeWeight((int) emotion);
    float elapsed = 0;
    float current = initial;

    while (elapsed < transitionDuration) {
      elapsed += Time.deltaTime;
      current = Mathf.Lerp(initial, value, elapsed/transitionDuration);
      skin.SetBlendShapeWeight((int) emotion, current);
      yield return null;
    }

    skin.SetBlendShapeWeight((int) emotion, value);
  }
}
