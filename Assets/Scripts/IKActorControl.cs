using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DitzelGames.FastIK;

public class IKActorControl : MonoBehaviour {
  public bool ikEnabled = false;

  public FastIKFabric rightHand;
  public FastIKFabric leftHand;

  void Update () {
    rightHand.enabled = leftHand.enabled = ikEnabled;
  }
}
