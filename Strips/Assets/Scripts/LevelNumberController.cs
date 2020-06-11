using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelNumberController : MonoBehaviour
{
    public List<Image> taskPoints;
    public static int levelNumber = 0;
    public static int taskNumber = 0;
    TextMeshProUGUI _level;

    void Start()
    {
        _level = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        _level.text = "Level " + levelNumber.ToString();

        for(int i = 0; i < taskNumber; ++i)
        {
            taskPoints[i].color = Color.red;
        }
    }
}
