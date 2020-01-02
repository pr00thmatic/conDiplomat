using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Emotions {
public class Blinker : MonoBehaviour {
  public RandomRange time;
  public SkinnedMeshRenderer skin;
  public float duration = 0.2f;
  public int blendShapeIndex = 0;

  public float _cooldown = 0;

  void Start () {
    _cooldown = time.Uniform;
    StartCoroutine(_Blink());
  }

  void Update () {
    _cooldown -= Time.deltaTime;

    if (_cooldown <= 0) {
      _cooldown = time.Uniform;
      StopAllCoroutines();
      StartCoroutine(_Blink());
    }
  }

  IEnumerator _Blink () {
    float weight = skin.GetBlendShapeWeight(blendShapeIndex);
    float elapsed = 0;
    float mid = duration/2f;

    while (elapsed < duration) {
      elapsed += Time.deltaTime;

      if (elapsed < mid) {
        skin.SetBlendShapeWeight(blendShapeIndex, Mathf.Lerp(weight, 100, elapsed / mid));
      } else {
        skin.SetBlendShapeWeight(blendShapeIndex, Mathf.Lerp(100,0, (elapsed - mid) / mid));
      }

      yield return null;
    }
  }
}
}
