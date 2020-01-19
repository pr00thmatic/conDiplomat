using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class RandomRange {
  public float Max { get => Mathf.Max(a, b); }
  public float Min { get => Mathf.Min(a, b); }

  public float a;
  public float b;
  public float Uniform {
    get => Random.Range(Min, Max);
  }

  public RandomRange () {}

  public RandomRange (float a, float b) {
    this.a = a; this.b = b;
  }

  public bool Contains (float value) {
    return Min <= value && value <= Max;
  }
}
