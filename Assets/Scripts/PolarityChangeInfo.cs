using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PolarityChangeInfo {
  public float polarity = 1;
  public float timestamp = 0;
  public float angleDistance = 0;
  public Vector3 forward;
  public Vector3 up;
}
