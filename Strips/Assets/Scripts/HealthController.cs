using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthController : MonoBehaviour
{
    public static int count;

    TextMeshProUGUI health;

    void Start()
    {
        count = 3;
        health = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        health.text = count.ToString();
    }
}
