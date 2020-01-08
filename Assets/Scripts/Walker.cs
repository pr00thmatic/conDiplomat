using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class Walker : MonoBehaviour {
  public NavMeshAgent agent;
  public Animator animator;

  void Reset () {
    agent = GetComponent<NavMeshAgent>();
    animator = GetComponentInChildren<Animator>();
  }
  
  void OnEnable () {
    animator.SetBool("seated", false);
  }

  void Update () {
    animator.SetFloat("speed", agent.velocity.magnitude / agent.speed);
  }
}
