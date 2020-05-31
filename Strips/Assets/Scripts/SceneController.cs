using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    static Stack<int> loadedScenes = new Stack<int>();

    public void LoadScene(string name)
    {
        loadedScenes.Push(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(name);
    }

    public void LoadSceneAfterWaiting(string name)
    {
        StartCoroutine(LoadSceneAfterDelay(name));
    }

    IEnumerator LoadSceneAfterDelay(string name)
    {
        yield return new WaitForSeconds(0.8f);
        LoadScene(name);
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

            if (nextScene < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextScene);
            }
        }
    }

    // public void PauseGame(string name)
    // {
    //     loadedScenes.Push(SceneManager.GetActiveScene().buildIndex);
    //     SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
    // }

    // public void ConttinueGame()
    // {
    //     if (loadedScenes.Count > 0)
    //     {
    //         SceneManager.UnloadSceneAsync(loadedScenes.Pop());
    //     }
    // }

    public void QuitGame()
    {
        Application.Quit();
    }
}
