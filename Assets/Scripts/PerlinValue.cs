using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PerlinValue {
  public Vector2 scale = new Vector2(1,1);
  public float Value {
    get => scale.y * Mathf.PerlinNoise((randomSeed + Time.time) * scale.x, 0);
  }
  public float randomSeed = 3;

  public void Initialize () {
    randomSeed = Random.Range(0, 100f);
  }
}
