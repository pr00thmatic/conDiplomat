using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IGrabMediator {
  void HandleGrab (SimulatedHand hand);
  void HandleRelease ();
}
