using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrainBumper : MonoBehaviour {
  public Vector2 timelapse = new Vector2(0.5f, 2);
  public Animator animator;
  public AudioClip[] bumps;

  void Start () {
    StartCoroutine(_EventuallyBump());
  }

  void Reset () {
    animator = GetComponent<Animator>();
  }

  IEnumerator _EventuallyBump () {
    while (true) {
      yield return new WaitForSeconds(Random.Range(timelapse.x, timelapse.y));
      animator.SetTrigger("bump");
      AudioSource[] speakers = GetComponentsInChildren<AudioSource>();
      speakers[Random.Range(0, speakers.Length)]
        .PlayOneShot(bumps[Random.Range(0, bumps.Length)]);
    }
  }
}
