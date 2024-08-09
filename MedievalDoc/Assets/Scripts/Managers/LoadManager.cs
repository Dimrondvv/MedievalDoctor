using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    public UnityEvent OnGameSceneLoaded = new UnityEvent();
    [SerializeField] string nameSceneToLoad;
    private void Awake()
    {
        App.Instance.GameplayCore.RegisterLoadManager(this);
    }
    private void OnDestroy()
    {
        App.Instance.GameplayCore.UnregisterLoadManager();
    }
    public void LoadGame()
    {
        StartCoroutine(SceneLoad(nameSceneToLoad));
    }

    IEnumerator SceneLoad(string sceneName)
    {
        yield return new WaitForEndOfFrame();

        AsyncOperation loadLevel = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        loadLevel.allowSceneActivation = false;
        while (loadLevel.progress < 0.9f)
        {
            yield return null;
        }
        loadLevel.allowSceneActivation = true;
        while (!loadLevel.isDone)
        {
            Debug.Log("Loading: " + loadLevel.progress);
            yield return null;
        }
        OnGameSceneLoaded.Invoke();
    }
}
