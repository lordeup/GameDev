using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthController : MonoBehaviour
{
    public static int count;

    TextMeshProUGUI _health;

    void Start()
    {
        count = 3;
        _health = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        _health.text = count.ToString();
    }
}
