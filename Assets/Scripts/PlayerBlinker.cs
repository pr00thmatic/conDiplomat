using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerBlinker : MonoBehaviour {
  public Animator animator;

  public void Blink (bool value) {
    animator.SetBool("closed eyes", value);
  }
}
