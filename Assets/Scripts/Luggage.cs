using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Luggage : MonoBehaviour {
    public List<Equipable> items = new List<Equipable>();
    public TextMesh text;

    void Update () {
        text.text = "";

        foreach (Equipable equiped in items) {
            text.text += equiped.name + "\n";
        }
    }
}
