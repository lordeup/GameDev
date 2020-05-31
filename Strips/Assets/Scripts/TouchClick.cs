using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchClick : MonoBehaviour
{
    static float _startTime;
    static float _endTime;
    const float holdTime = 0.5f;

    void Start() 
    {
        ResetTime();
    }

    void ResetTime()
    {
        _startTime = 0f;
        _endTime = 0f;
    }
    
    public void OnPointerUp()
    {
        _endTime = Time.time;
    }

    public void OnPointerDown()
    {
        _startTime = Time.time;
    }

    public bool IsLongTouch() 
    {
        if (_endTime - _startTime > holdTime)
        {
            ResetTime();
            return true;
        }

        return false;
    }
}
