using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class Mesera : MonoBehaviour, IScriptPiece {
  public event System.Action onFinished;

  public ConversationScript askForDrinks;

  public Animator animator;
  public Transform wait;
  public Transform client;
  public NavMeshAgent agent;

  void Start () {
    animator.SetBool("seated", false);
  }

  public void Execute () {
    StartCoroutine(_AskForBeberages());
  }

  IEnumerator _AskForBeberages () {
    agent.SetDestination(client.position);
    while (agent.pathPending || agent.remainingDistance > 0.2f) {
      yield return null;
    }
  }
}
