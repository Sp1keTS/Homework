using System;
using TMPro;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] DiceScript[] dices;
    [SerializeField] DiceRoller diceRoller;
    
    private int counter = 0;

    private void Update()
    {
        foreach (DiceScript dice in dices)
        {
            counter += dice.CurrentSide;
        }
        text.text = counter.ToString() + "\n" +"Текущая клавиша:" + diceRoller.RollKey;
        counter = 0;
    }
}
