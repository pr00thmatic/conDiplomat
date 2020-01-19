using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EmotionalStatus : MonoBehaviour {
  public const float sad = -30;
  public const float neutral = 40;

  public EmotionManager manager;

  [Range(0,1)]
  public float honesty = 0.5f;
  [Range(0,1)]
  public float selfControl = 0.3f;
  [Range(0,1)]
  public float clampedHonesty = 0.4f;

  public RandomRange fearTimer = new RandomRange(1.5f, 2);
  public RandomRange okaynessTimer = new RandomRange(3, 5);
  public RandomRange hiddenTimer = new RandomRange(5, 8);

  [Range(-100, 100)]
  public float okayness = 0;
  [Range(-100, 100)]
  public float fear = 0;

  public float _cooldown = 0;

  void Update () {
    if (_cooldown <= 0) {
      ResetDisplay();
    }

    _cooldown -= Time.deltaTime;
  }

  public void ResetDisplay () {
    bool honest = Random.Range(0,1f) < honesty;
    bool controlled = Random.Range(0,1f) < selfControl;

    if (controlled) {
      DisplayOkayness(honest? 1: clampedHonesty);
      _cooldown = honest? okaynessTimer.Uniform: hiddenTimer.Uniform;
    } else {
      DisplayFear(honest? 1: clampedHonesty);
      _cooldown = fearTimer.Uniform;
    }
  }

  public void DisplayOkayness (float clamp = 1) {
    manager.ResetEmotions();
    Emotion emotion;
    float value;

    if (okayness < sad) {
      emotion = Emotion.Mad;
      value = Util.Map(sad, -100, okayness);
    } else if (okayness < neutral) {
      emotion = Emotion.Sad;
      value = Util.Map(neutral, sad, okayness);
    } else {
      emotion = Emotion.Happy;
      value = Util.Map(neutral, 100, okayness);
    }

    manager.SetEmotion(emotion, Mathf.Clamp(value * 100, 0, clamp * 100));
  }

  public void DisplayFear (float clamp = 1) {
    manager.ResetEmotions();
    Emotion emotion;
    float value;

    if (fear < 0) {
      emotion = Emotion.Mock;
      value = Util.Map(0, -100, fear);
    } else {
      emotion = Emotion.Fear;
      value = Util.Map(0, 100, fear);
    }

    manager.SetEmotion(emotion, Mathf.Clamp(value * 100, 0, clamp * 100));
  }
}
