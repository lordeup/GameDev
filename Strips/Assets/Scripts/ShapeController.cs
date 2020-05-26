using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeController : MonoBehaviour
{
    void OnMouseDown()
    {
        transform.Rotate(0f, 0f, 90f);
    }
}
