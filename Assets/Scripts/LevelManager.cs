using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : Singleton<LevelManager> {
  public string loadingSceneName;

  public void LoadLevel (string levelName) {
    levelName = levelName;
    StartCoroutine(_EventuallyLoadLevel(levelName));
  }

  IEnumerator _EventuallyLoadLevel (string levelName) {
    AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(loadingSceneName);
    float elapsed = 0;

    while (loadingOperation.isDone == false || elapsed < 3) {
      elapsed += Time.deltaTime;
      yield return null;
    }

    loadingOperation = SceneManager.LoadSceneAsync(levelName);
  }
}
