using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class NextTriggerer {
  public bool triggersNext = false;
  public bool waitsOnFinish = false;
  public float time = 0;
}
