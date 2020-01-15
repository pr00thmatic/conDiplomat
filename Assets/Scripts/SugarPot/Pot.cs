using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SugarPot {
public class Pot : MonoBehaviour {
  public Transform spoonPosition;

  public void SetSpoon (Spoon spoon) {
    spoon.transform.parent = spoonPosition;
  }
}
}
