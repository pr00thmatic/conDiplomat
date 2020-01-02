using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LipSyncher : MonoBehaviour {
  public SkinnedMeshRenderer skin;
  public AudioSource voice;
  public int blendShapeIndex;
  public int bufferLengthFactor = 10;
  public float amplitudeFactor = 1000;

  float[] _data;

  void Update () {
    if (!voice.clip) {
      skin.SetBlendShapeWeight(blendShapeIndex, 0);
      return;
    }

    _data = new float[bufferLengthFactor * 1024];

    if (voice.isPlaying) {
      voice.clip.GetData(_data, voice.timeSamples);
    } else {
      _data = new float[] { 0 };
    }

    float value = 0;
    foreach (float sample in _data) {
      value += Mathf.Abs(sample);
    }
    skin.SetBlendShapeWeight(blendShapeIndex, Mathf.Clamp((value/(float) _data.Length) * amplitudeFactor, 0, 100));
  }
}
