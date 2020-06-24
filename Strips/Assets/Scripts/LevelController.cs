using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public List<Image> hideShapes;
    public List<Image> lines;

    public List<Image> shapes;
    public Color colorShape;

    public GameObject winningPanel;
    public GameObject losingPanel;

    Image _activeLine;
    Image _activeShape;
    TouchClick _touchClick;
    RandomController _randomController;
    SceneController _sceneController;
    bool _isUpdate;

    const int _totalTaskNumber = 4;

    Color32 _lineColor;

    bool _isLose;

    void Start()
    {
        _touchClick = gameObject.AddComponent<TouchClick>();
        _randomController = gameObject.AddComponent<RandomController>();
        _sceneController = gameObject.AddComponent<SceneController>();
        _isUpdate = true;
        _isLose = false;
        _lineColor = new Color32(232, 14, 12, 255);

        String value = Utils.GetRegexMatchValue(_sceneController.GetActiveScene().name);

        if (value.Length > 2)
        {
            LevelNumberController.levelNumber = Int32.Parse(value[0].ToString());
            LevelNumberController.taskNumber = Int32.Parse(value[value.Length - 1].ToString());
        }

        foreach(Image shape in shapes)
        {
            Utils.ChangeColorImage(shape, colorShape);
        }
    }

    void Update()
    {
        if (_isUpdate)
        {
            ChangeActive(lines.Count);
        }

        if (!_isLose && (HealthController.count == 0 || TimerController.currentTime == 0f))
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

        if (LevelNumberController.taskNumber < _totalTaskNumber)
        {
            _sceneController.LoadNextSceneAfterWaiting();
        }
        else
        {
            _sceneController.panel = winningPanel;
            _sceneController.StopGame();
        }
    }

    void LevelLose()
    {
        _sceneController.panel = losingPanel;
        _sceneController.StopGame();
        _isLose = true;
    }

    void ChangeActive(int maxNumber)
    {
        _isUpdate = false;

        if (_randomController.GetNumbersSize() < maxNumber)
        {
            int randomNumber = _randomController.GetRandomNunber(maxNumber);

            _activeShape = hideShapes[randomNumber];
            
            _activeLine = lines[randomNumber];
            Utils.ChangeEnabledImage(_activeLine, true);
            Utils.ChangeColorImage(_activeLine, _lineColor);
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
            Utils.RotateImage(image);
        }
    }

    void DragImage(Image image)
    {
        Vector3 activeShapeRotation = _activeShape.transform.localRotation.eulerAngles;
        Vector3 imageRotation = image.transform.localRotation.eulerAngles;
    
        if (activeShapeRotation.z == imageRotation.z && _activeShape.sprite.name == image.sprite.name)
        {
            image.transform.localPosition = _activeShape.transform.localPosition;
            Utils.ChangeEnabledImage(_activeLine, false);
            image.transform.GetComponent<EventTrigger>().enabled = false;
            _isUpdate = true;
        }
        else
        {
            --HealthController.count;
        }
    }
}
