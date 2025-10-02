using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] TMP_Text pointCounter;
    [SerializeField] TMP_Text diceCounter;
    [SerializeField] TMP_Text targetPointsCounter;
    [SerializeField] List<DiceScript> dices;
    [SerializeField] DiceRoller diceRoller;
    private int lastPoints;
    public List<DiceScript> Dices { get => dices; set => dices = value; }
    private int pointCount;
    private int diceCount;
    
    public int Points { get => lastPoints; set => lastPoints = value; }

    private void Update()
    {
        foreach (DiceScript dice in dices)
        {
            pointCount += dice.CurrentSide;
            diceCount += 1;
        }
        pointCounter.text = "Очков с броска получено:" + pointCount;
        diceCounter.text = "Кубов приобретено: " + diceCount;
        lastPoints = pointCount;
        pointCount = 0;
        diceCount = 0;
    }
}
