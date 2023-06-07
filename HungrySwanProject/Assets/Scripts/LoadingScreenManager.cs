using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenManager : MonoBehaviour
{
    public string sceneToLoad;

    private void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation pleaseWork = SceneManager.LoadSceneAsync(sceneToLoad);

        pleaseWork.allowSceneActivation = false;

        while (!pleaseWork.isDone)
        {
            if (pleaseWork.progress >= 0.9f)
            {
                pleaseWork.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
