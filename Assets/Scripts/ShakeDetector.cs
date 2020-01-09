using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ShakeDetector {
  public event System.Action onShake;

  public float timeTolerance = 0.8f;
  public float speedTolerance = 10; // degrees per second
  public float lastValue;
  public float lastShake = 0;

  public void Update (float value) {
    if (Mathf.Abs(value) > speedTolerance &&
        Mathf.Sign(value) != Mathf.Sign(lastValue)) {
      float difference = Time.time - lastShake;
      lastShake = Time.time;

      if (difference < timeTolerance && onShake != null) {
        onShake();
      }
    }

    lastValue = value;
  }
}
