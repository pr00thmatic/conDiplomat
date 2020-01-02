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
}
