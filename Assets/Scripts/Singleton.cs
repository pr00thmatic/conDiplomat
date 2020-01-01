using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Singleton<T> : MonoBehaviour where T:MonoBehaviour {
  public static T Instance {
    get {
      if (!_instance) {
        _instance = GameObject.FindObjectOfType<T>();
      }

      return _instance;
    }
  }
  static T _instance;

  void Awake () {
    if (Instance != this) {
      Debug.LogWarning("an instance of this already exist", this);
      Destroy(this);
    } else {
      DontDestroyOnLoad(gameObject);
    }
  }
}
