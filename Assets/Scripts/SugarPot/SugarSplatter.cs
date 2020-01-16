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
  public float angleTolerance = 10;
  public Transform target;
  public float angle;
  public float maxAngle = 50;
  public bool canSpill = false;
  public ParticleSystem spillEffect;
  public float maxTime = 2;

  float _spillingTime = 0;
  Coroutine _fill;

  void Update () {
    skin.SetBlendShapeWeight(0,(1-value) * 100);
    if (canSpill) {
      UpdateSpill();
    }

    if (_spillingTime > 0) {
      _spillingTime -= Time.deltaTime;
      if (!spillEffect.isPlaying) {
        spillEffect.Play();
      }
    } else {
      _spillingTime = 0;
      spillEffect.Stop();
    }
  }

  public void UpdateSpill () {
    angle = Vector3.Angle(Vector3.up, target.up);

    if (angle > angleTolerance) {
      float spillValue = Mathf.Lerp(1,0, (angle-angleTolerance)/maxAngle);
      if (spillValue < value) {
        _spillingTime += (value-spillValue) * maxTime;
        _spillingTime = Mathf.Max(0.2f, _spillingTime);
        value = spillValue;
        spillEffect.transform.parent.forward =
          Vector3.ProjectOnPlane(target.up, Vector3.up);
        spillEffect.transform.parent.forward =
          Vector3.ProjectOnPlane(spillEffect.transform.parent.forward, target.up);
      }
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
