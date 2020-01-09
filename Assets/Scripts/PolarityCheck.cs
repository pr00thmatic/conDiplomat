using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PolarityCheck {
  public const int bufferLength = 10;
  public const float timeTollerance = 1;

  public float average = 0;
  public int current = 0;

  public int polarityChanges = 0;

  float[] _values = new float[bufferLength];
  float _lastTime = 0;
  float _lastAverage;

  public void Initialize (float value) {
    average = value;
    _lastTime = Time.time;
  }

  public void Update (float value) {
    // current = (current + 1) % bufferLength;
    // values[current] = value;
    // average += value/(float) bufferLength;
    // average -= values[(current+bufferLength+1)%bufferLength]/(float) bufferLength;

    // if (Mathf.Sign(_lastAverage) != Mathf.Sign(average)) {
    //   if (
    //   polarityChanges++;
    //   }

    // _lastAverage = average;
  }
}
