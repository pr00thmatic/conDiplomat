using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "gift", menuName = "Definitions/Gift Definition")]
public class GiftDefinition : ScriptableObject {
  public string name;
  public AudioClip dialogue;
}
