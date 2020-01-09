using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class YesNoReader : MonoBehaviour {
  public TextMesh yes;
  public TextMesh no;

  public void Yes () {
    yes.text = (int.Parse(yes.text) + 1) + "";
  }

  public void No () {
    no.text = (int.Parse(no.text) + 1) + "";
  }
}
