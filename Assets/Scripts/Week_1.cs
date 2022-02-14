using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Week_1 : MonoBehaviour
{
    public int numberOne = 5;
    public int numberTwo = 9;
    public int numb4 = 6;
    
    void Start()
    {
        AddNumbers(numberOne, numberTwo);
        AddNumbers(4, 6);
        AddNumbers(numb4, numberOne);
    }

  void AddNumbers(int _number1, int _number2)
    {
        int result = _number1 + _number2;
        print(result);
    }
}
