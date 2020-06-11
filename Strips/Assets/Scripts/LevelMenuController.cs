using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenuController : MonoBehaviour
{
    public Button button;

    public string levelName;

    SceneController _sceneController;
    static HashSet<int> winLevels = new HashSet<int>();

    void Start()
    {
        _sceneController = gameObject.AddComponent<SceneController>();

        int buildIndex = _sceneController.GetBuildIndexByScenePath(levelName);
        button.interactable = winLevels.Contains(buildIndex) ? true : false;
    }

     public static void AddWinLevels(int winLevel)
    {
        winLevels.Add(winLevel);
    }
}
