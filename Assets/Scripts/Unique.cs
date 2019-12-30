using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unique : MonoBehaviour {
    // TODO: turn it off only while on luggage.
    public void TurnOffCollidersForABit () {
        Util.SetColliders(this.transform, false);
        StartCoroutine(_DeferredRestoreCollisions());
    }

    IEnumerator _DeferredRestoreCollisions () {
        yield return new WaitForSeconds(0.25f);
        Util.SetColliders(this.transform, true);
    }
}
