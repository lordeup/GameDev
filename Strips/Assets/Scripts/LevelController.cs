using System.Collections;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public List<Image> hideShapes;
    public List<Image> lines;

    Image _activeLine;
    Image _activeShape;
    TouchClick _touchClick;
    RandomController _randomController;
    SceneController _sceneController;
    bool _isUpdate;

    const int _totalTaskNumber = 4;

    void Start()
    {
        _touchClick = gameObject.AddComponent<TouchClick>();
        _randomController = gameObject.AddComponent<RandomController>();
        _sceneController = gameObject.AddComponent<SceneController>();
        _isUpdate = true;

        String value = GetRegexMatchValue(_sceneController.GetActiveScene().name);

        if (value.Length > 2)
        {
            LevelNumberController.levelNumber = Int32.Parse(value[0].ToString());
            LevelNumberController.taskNumber = Int32.Parse(value[value.Length - 1].ToString());
        }
    }

    void Update()
    {
        if (_isUpdate)
        {
            ChangeActive(lines.Count);
        }

        if (HealthController.count == 0 || TimerController.currentTime == 0f)
        {
            LevelLose();
        }
    }

    void LevelWin()
    {
        int nextBuildIndex = _sceneController.GetActiveScene().buildIndex + 1;

        if (nextBuildIndex < _sceneController.GetSceneCountInBuildSettings())
        {
            LevelMenuController.AddWinLevels(nextBuildIndex);
        }

        foreach (Image line in lines)
        {
            ChangeColorImage(line, Color.green);
            ChangeEnabledImage(line, true);
        }

        if (LevelNumberController.taskNumber < _totalTaskNumber)
        {
            _sceneController.LoadNextSceneAfterWaiting();
        }
        else
        {
            _sceneController.LoadSceneAfterWaiting("Winning");
        }
    }

    void LevelLose()
    {
        _sceneController.LoadSceneAfterWaiting("Losing");
    }

    void ChangeActive(int maxNumber)
    {
        _isUpdate = false;

        if (_randomController.GetNumbersSize() < maxNumber)
        {
            int randomNumber = _randomController.GetRandomNunber(maxNumber);

            _activeShape = hideShapes[randomNumber];
            
            _activeLine = lines[randomNumber];
            ChangeEnabledImage(_activeLine, true);
            ChangeColorImage(_activeLine, Color.red);
        }
        else
        {
            LevelWin();
        }
    }

    public void OnClickImage(Image image)
    {
        if (_touchClick.IsLongTouch())
        {
            DragImage(image);
        }
        else
        {
            RotateImage(image);
        }
    }

    void ChangeEnabledImage(Image image, bool value)
    {
        image.enabled = value;
    }

    void ChangeColorImage(Image image, Color color)
    {
        image.color = color;
    }

    void RotateImage(Image image)
    {
        image.transform.Rotate(0f, 0f, 90f);
    }

    void DragImage(Image image)
    {
        Vector3 activeShapeRotation = _activeShape.transform.localRotation.eulerAngles;
        Vector3 imageRotation = image.transform.localRotation.eulerAngles;
    
        if (activeShapeRotation.z == imageRotation.z && _activeShape.sprite.name == image.sprite.name)
        {
            image.transform.localPosition = _activeShape.transform.localPosition;
            ChangeEnabledImage(_activeLine, false);
            image.transform.GetComponent<EventTrigger>().enabled = false;
            _isUpdate = true;
        }
        else
        {
            --HealthController.count;
        }
    }

    String GetRegexMatchValue(string str)
    {
        return Regex.Match(str, @"[\d\.]+").Value;
    }
}
