using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Outline))]
public class Grabbable : MonoBehaviour {
    public void SetHighlight (bool value) {
        GetComponent<Outline>().enabled = value;
    }
}
