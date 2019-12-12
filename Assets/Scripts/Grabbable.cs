using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Outline))]
public class Grabbable : MonoBehaviour {
    public Outline highlighter;
    public bool Highlighted { get => highlighter.enabled; }

    public void SetHighlight (bool value) {
        highlighter.enabled = value;
    }
}
