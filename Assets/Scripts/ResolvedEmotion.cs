using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResolvedEmotion : MonoBehaviour {
  public Emotion CurrentEmotion { get => available[status]; }

  public int status;
  public List<Emotion> available;
}
