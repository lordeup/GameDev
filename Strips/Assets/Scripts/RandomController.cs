using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class RandomController : MonoBehaviour
{
    List<int> numbers = new List<int>();

    public int GetRandomNunber(int maxNumber)
    {
        while (true)
        {
            int number = Random.Range(0, maxNumber);
            if (!numbers.Contains(number))
            {
                numbers.Add(number);
                break;
            }
        }

        return GetNumbersSize() > 0 ? numbers.Last() : 0;
    }

    public int GetNumbersSize() 
    {
        return numbers.Count;
    }
}
