using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PerlinLevitation : MonoBehaviour {
  Vector3 _localPosition;
  public PerlinValue x;
  public PerlinValue y;
  public PerlinValue z;

  void OnEnable () {
    _localPosition = transform.localPosition;
    x.Initialize();
    y.Initialize();
    z.Initialize();
  }

  void Update () {
    transform.localPosition = _localPosition + new Vector3(x.Value, y.Value, z.Value);
  }
}
