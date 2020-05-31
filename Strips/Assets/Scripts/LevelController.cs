using System.Collections;
using System.Collections.Generic;
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

    void Start()
    {
        _touchClick = gameObject.AddComponent<TouchClick>();
        _randomController = gameObject.AddComponent<RandomController>();
        _sceneController = gameObject.AddComponent<SceneController>();
        _isUpdate = true;
    }

    void Update()
    {
        if (_isUpdate)
        {
            ChangeActive(lines.Count);
        }

        if (HealthController.count == 0)
        {
            LevelLose();
        }
    }

    void LevelWin()
    {
        ScoreController.count += 10;
        _sceneController.LoadSceneAfterWaiting("Winning");
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
            _activeLine.enabled = true;
            _activeLine.color = Color.red;
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
            _activeLine.enabled = false;
            image.transform.GetComponent<EventTrigger>().enabled = false;
            _isUpdate = true;
        }
        else
        {
            --HealthController.count;
        }
    }
}
