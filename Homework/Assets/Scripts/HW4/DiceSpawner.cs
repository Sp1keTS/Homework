using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DiceSpawner : MonoBehaviour
{
    
    [SerializeField] Button spawnButton;
    [SerializeField] GameObject dicePrefab;
    [SerializeField] DiceRoller diceRoller;
    [SerializeField] GameUI gameUI;
    [SerializeField] private float minDiv = -9;
    [SerializeField] private float maxDiv = 9;
    private void Awake()
    {
        spawnButton.onClick.AddListener(SpawnDice);
    }
    
    public void SpawnDice()
    {
        var position = new Vector3(Random.Range(minDiv,maxDiv), 1, Random.Range(minDiv,maxDiv));
        DiceScript dice = Instantiate(dicePrefab, position, Quaternion.identity).GetComponent<DiceScript>();
        diceRoller.Dices.Add(dice.GetComponent<Rigidbody>());
        gameUI.Dices.Add(dice.GetComponent<DiceScript>());
    }
}
