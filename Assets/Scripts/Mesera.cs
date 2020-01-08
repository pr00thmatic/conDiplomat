using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class Mesera : MonoBehaviour {
  public Animator animator;

  void Start () {
    animator.SetBool("seated", false);
  }

  // public void Execute () {
  //   StartCoroutine(_AskForBeberages());
  // }

  // IEnumerator _AskForBeberages () {
  //   agent.SetDestination(client.position);
  //   while (agent.pathPending || agent.remainingDistance > 0.2f) {
  //     yield return null;
  //   }
  // }
}
