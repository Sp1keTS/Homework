using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private int minNeutralPoints;
    [SerializeField] private int minPositivePoints;
    [SerializeField] private int minOffset = 3;
    [SerializeField] private int maxOffset = 6;
    [SerializeField] private Button rollButton;
    [SerializeField] private TMP_Text TargetValues;
    [SerializeField] private DiceRoller diceRoller;
    [SerializeField] private GameUI gameUI;
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private TMP_InputField inputMinNeutral;
    [SerializeField] private TMP_InputField inputMinPositive;
    
    private int diceCount;

    private void Awake()
    {
        rollButton.onClick.AddListener(GetEdgeValues);
    }

    private void Update()
    {
        if (diceRoller.AllDicesStopped)
        {
            OnAllDicesStopped();
        }
        else
        {
            resultText.text = "...";
        }
    }

    private void GetEdgeValues()
    {
        diceCount = diceRoller.Dices.Count;
        try
        {
            minNeutralPoints = int.Parse(inputMinNeutral.text);
        }
        catch
        {
            minNeutralPoints = Random.Range(diceCount , diceCount * minOffset);
        }

        try
        {
            minPositivePoints = int.Parse(inputMinPositive.text);
        }
        catch
        {
            minPositivePoints = Random.Range(diceCount *minOffset, diceCount * maxOffset);
        }

        
        TargetValues.text = $"Минимальные очки: {minNeutralPoints}, победные очки: {minPositivePoints}";
    }

    private void OnAllDicesStopped()
    {

        if (gameUI.Points >= minPositivePoints)
        {
            resultText.text = "Поздравляем, вы победили! :D";
        }
        else if (gameUI.Points >= minNeutralPoints)
        {
            resultText.text = "Результаты не однозначны!";
        }
        else
        {
            resultText.text = "Вы проиграли! :С";
        }
    }
}
