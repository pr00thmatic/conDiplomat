using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SugarSplatter : MonoBehaviour {
  public SkinnedMeshRenderer skin;
  public float value = 0;
  public AudioClip[] sugar;
  // public AudioClip[] sugarLow;
  public AudioSource voice;
  public float fillingTime = 0.25f;
  public float angleTollerance = 10;
  public Transform target;
  public float angle;
  public float maxAngle = 50;

  Coroutine _fill;

  void Update () {
    skin.SetBlendShapeWeight(0,(1-value) * 100);
    angle = Vector3.Angle(Vector3.up, target.up);

    if (angle > 10) {
      value = Mathf.Lerp(1,0, (angle-10)/maxAngle);
      // value = Mathf.Min(value, Mathf.Lerp(1,0, (angle-10)/maxAngle));
    }
  }

  public void Fill () {
    if (_fill != null) StopCoroutine(_fill);
    _fill = StartCoroutine(_Fill());
  }

  IEnumerator _Fill () {
    float elapsed = 0;
    float initial = value;

    voice.PlayOneShot(sugar[Random.Range(0, sugar.Length)]);
    while (elapsed < fillingTime) {
      elapsed += Time.deltaTime;
      value = Mathf.Lerp(initial, 1, elapsed/fillingTime);
      yield return null;
    }

    value = 1;
    _fill = null;
  }
}
