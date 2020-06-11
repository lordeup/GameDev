using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    static Stack<int> loadedScenes = new Stack<int>();

    public void LoadScene(string name)
    {
        AddSceneToLoadedScenes(GetActiveScene().buildIndex);
        SceneManager.LoadScene(name);
    }

    public void LoadSceneAfterWaiting(string name)
    {
        StartCoroutine(LoadSceneAfterDelay(name));
    }

    public void LoadNextSceneAfterWaiting()
    {
        AddSceneToLoadedScenes(GetActiveScene().buildIndex);
        StartCoroutine(LoadNextSceneAfterDelay());
    }

    IEnumerator LoadSceneAfterDelay(string name)
    {
        yield return new WaitForSeconds(0.5f);
        LoadScene(name);
    }

    IEnumerator LoadNextSceneAfterDelay()
    {
        yield return new WaitForSeconds(0.5f);
        LoadNextScene();
    }

    public void LoadPreviousScene()
    {
        if (loadedScenes.Count > 0)
        {
            SceneManager.LoadScene(loadedScenes.Pop());
        }
    }

    public void LoadNextScene()
    {
        if (loadedScenes.Count > 0)
        {
            int nextScene = loadedScenes.Peek() + 1;

            if (nextScene < GetSceneCountInBuildSettings())
            {
                SceneManager.LoadScene(nextScene);
            }
        }
    }

    public void PauseGame(string name)
    {
        StoppingTime();

        int buildIndexLevel = GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);

        AddSceneToLoadedScenes(GetSceneByName(name).buildIndex);
        AddSceneToLoadedScenes(buildIndexLevel);
    }

    IEnumerator UnloadScene(int buildIndex)
    {
        yield return null;
        SceneManager.UnloadSceneAsync(buildIndex);
    }

    public void ContinueGame()
    {
        if (loadedScenes.Count > 1)
        {
            StartCoroutine(UnloadScene(loadedScenes.Pop()));
            SceneManager.UnloadSceneAsync(loadedScenes.Pop());
        }
    }

    public Scene GetActiveScene()
    {
        return SceneManager.GetActiveScene();
    }

    public Scene GetSceneByName(string name)
    {
        return SceneManager.GetSceneByName(name);
    }

    public int GetBuildIndexByScenePath(string name)
    {
        return SceneUtility.GetBuildIndexByScenePath(name);
    }

    public int GetSceneCountInBuildSettings()
    {
        return SceneManager.sceneCountInBuildSettings;
    }

    public void StartingTime()
    {
        Time.timeScale = 1f;
    }

    void StoppingTime()
    {
        Time.timeScale = 0f;
    }

    void AddSceneToLoadedScenes(int buildIndex)
    {
        loadedScenes.Push(buildIndex);
    }
}
