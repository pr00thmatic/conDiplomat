using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : Singleton<LevelManager> {
  public string loadingSceneName;
  public float artificialLoadingTime = 10;
  public Equipable gift;

  public void SetGift (Equipable gift) {
    DontDestroyOnLoad(gift.gameObject);
    gift.transform.parent = transform;
    this.gift = gift;
    gift.body.isKinematic = true;
    Util.ResetBody(gift.body);
    gift.GetComponent<BunHolder>().bun.parent = gift.transform;
    gift.gameObject.SetActive(false);
  }

  public void LoadLevel (string levelName) {
    StartCoroutine(_EventuallyLoadLevel(levelName));
  }

  IEnumerator _EventuallyLoadLevel (string levelName) {
    AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(loadingSceneName);
    float elapsed = 0;

    while (loadingOperation.isDone == false || elapsed < artificialLoadingTime) {
      elapsed += Time.deltaTime;
      yield return null;
    }

    loadingOperation = SceneManager.LoadSceneAsync(levelName);
    while (loadingOperation.isDone == false) {
      yield return null;
    }
    PrepareLevel();
  }

  public void PrepareLevel () {
    Transform giftHolder = GameObject.FindWithTag("gift placeholder").transform;
    gift.transform.parent = giftHolder;
    gift.transform.localPosition = Vector3.zero;
    gift.transform.localRotation = Quaternion.identity;
    gift.body.isKinematic = false;
    gift.gameObject.SetActive(true);
  }
}
