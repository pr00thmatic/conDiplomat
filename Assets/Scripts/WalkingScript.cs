using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class WalkingScript : MonoBehaviour, IScriptPiece {
  public NextTriggerer Triggerer { get => _triggerer; } [SerializeField] NextTriggerer _triggerer;
  public bool waitsForFinish;
  public Transform[] destination;
  public NavMeshAgent agent;
  public Animator animator;

  int _nextOne = 0;

  void Start () {
    animator = agent.GetComponentInChildren<Animator>();
  }

  void Reset () {
    agent = GetComponentInParent<NavMeshAgent>();
    animator = agent.GetComponentInChildren<Animator>();
  }

  public void Execute () {
    StartCoroutine(_StartWalking());
  }

  IEnumerator _StartWalking () {
    yield return new WaitForSeconds(Triggerer.delay);
    animator.SetBool("seated", false);

    foreach (Transform d in destination) {
      agent.updateRotation = true;
      agent.SetDestination(d.position);
      while (agent.pathPending || agent.remainingDistance > 0.2f) {
        animator.SetFloat("speed", agent.velocity.magnitude / agent.speed);
        yield return null;
      }
      agent.updateRotation = false;
      agent.transform.forward = d.transform.forward;
    }

    Triggerer.TriggerFinish(this);
  }
}
