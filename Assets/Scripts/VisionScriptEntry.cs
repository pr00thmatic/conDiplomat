using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using HumanParts;

[System.Serializable]
public class VisionScriptEntry : ScriptEntry {
  public Transform target;
  public HumanPartTarget optionalPart;
}
