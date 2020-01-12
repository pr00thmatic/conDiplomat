using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class WalkingScript : MonoBehaviour, IScriptPiece {
  public NextTriggerer Triggerer { get => _triggerer; } [SerializeField] NextTriggerer _triggerer;
  public bool waitsForFinish;
  public Transform[] destination;
  public NavMeshAgent agent;

  int _nextOne = 0;

  void Reset () {
    agent = GetComponentInParent<NavMeshAgent>();
  }

  public void Execute () {
    StartCoroutine(_StartWalking());
  }

  IEnumerator _StartWalking () {
    foreach (Transform d in destination) {
      agent.updateRotation = true;
      agent.SetDestination(d.position);
      while (agent.pathPending || agent.remainingDistance > 0.2f) {
        yield return null;
      }
      agent.updateRotation = false;
      agent.transform.forward = d.transform.forward;
    }

    Triggerer.TriggerFinish(this);
  }
}
