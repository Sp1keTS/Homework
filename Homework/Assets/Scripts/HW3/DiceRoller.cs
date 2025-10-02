using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DiceRoller : MonoBehaviour
{
    
    private GameInput gameInput;
    
    [SerializeField] private List<Rigidbody> dices; 
    [SerializeField] private float minForce;
    [SerializeField] private float maxForce;
    [SerializeField] private float minDev;
    [SerializeField] private float maxDev;
    [SerializeField] private Key rollKey = Key.R;
    [SerializeField] Button rollButton;
    [SerializeField] TMP_Text rollButtonText;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] private float velocityThreshold = 0.01f;
    [SerializeField] private float initialDelay = 0.5f; // Задержка перед первой проверкой
    [SerializeField] private float checkDelay = 0.1f; // Задержка между последующими проверками
    
    public List<Rigidbody> Dices { get => dices; set => dices = value; }
    public Key RollKey { get => rollKey; set => rollKey = value; }
    private int diceCount;
    private bool allDicesStopped;

    public bool AllDicesStopped { get => allDicesStopped; }
    
    private void Awake()
    {
        if (dices == null)
            dices = new List<Rigidbody>();
        rollButton.onClick.AddListener(RollDice);
        gameInput = new GameInput();
        ApplyKeyBinding();
        gameInput.Enable();
    }

    private void ApplyKeyBinding()
    {
        gameInput.RollDice.Rolling.ApplyBindingOverride(0, $"<Keyboard>/{rollKey}");
        rollButtonText.text = $" Бросить кости [{rollKey}] ";
    }

    private void OnEnable()
    {
        gameInput.RollDice.Rolling.performed += RollOnPerformed;
    }

    private void RollOnPerformed(InputAction.CallbackContext obj)
    {
        rollButton.onClick.Invoke();
    }

    public void RollDice()
    {
        allDicesStopped = false;
        try
        {
            diceCount = int.Parse(inputField.text);
        }
        catch
        {
            diceCount = 0;
        }
        
        if (AreDicesMoving())
        {
            return;
        }
        
        foreach(Rigidbody dice in dices)
        {
            if (diceCount <= 0) break;
            
            diceCount -= 1; 
            
            dice.AddForce(Vector3.up * Random.Range(minForce, maxForce), ForceMode.Impulse);
            Vector3 torque = new Vector3(
                Random.Range(minDev, maxDev),
                Random.Range(minDev, maxDev), 
                Random.Range(minDev, maxDev)
            );
            dice.AddTorque(torque, ForceMode.Impulse);
        }

        StartCoroutine(WaitForDicesToStop());
    }

    private System.Collections.IEnumerator WaitForDicesToStop()
    {
        
        yield return new WaitForSeconds(initialDelay);
        
        while (AreDicesMoving())
        {
            yield return new WaitForSeconds(checkDelay);
        }
        
        allDicesStopped = true;
        
    }

    public bool AreDicesMoving()
    {
        foreach(Rigidbody dice in dices)
        {
            if (dice.linearVelocity.magnitude > velocityThreshold || 
                 dice.angularVelocity.magnitude > velocityThreshold)
            {
                return true;
            }
        }
        return false;
    }
    

    private void OnDisable()
    {
        gameInput.RollDice.Rolling.performed -= RollOnPerformed;
    }

    private void OnValidate()
    {
        if (gameInput != null)
        {
            ApplyKeyBinding();
        }
    }
}