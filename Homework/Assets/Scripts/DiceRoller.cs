using UnityEngine;
using UnityEngine.InputSystem;

public class DiceRoller : MonoBehaviour
{
    private GameInput gameInput;
    
    [SerializeField] private Rigidbody[] dices;
    [SerializeField] private float minForce;
    [SerializeField] private float maxForce;
    [SerializeField] private float minDev;
    [SerializeField] private float maxDev;
    [SerializeField] private  Key rollKey = Key.R;
    
    public Key RollKey { get => rollKey; set => rollKey = value; }
    private void Awake()
    {
        gameInput = new GameInput();
        ApplyKeyBinding();
        gameInput.Enable();
    }

    private void ApplyKeyBinding()
    {
        gameInput.RollDice.Rolling.ApplyBindingOverride(0, $"<Keyboard>/{rollKey}");
    }

    private void OnEnable()
    {
        gameInput.RollDice.Rolling.performed += RollOnPerformed;
    }

    private void RollOnPerformed(InputAction.CallbackContext obj)
    {
        RollDice();
    }

    private void RollDice()
    {
        foreach(Rigidbody dice in dices)
        {
            if (dice.linearVelocity.magnitude < 1f)
            {
                dice.AddForce(Vector3.up * Random.Range(minForce, maxForce), ForceMode.Impulse);
                Vector3 torque = new Vector3(
                    Random.Range(minDev, maxDev),
                    Random.Range(minDev, maxDev), 
                    Random.Range(minDev, maxDev)
                );
                dice.AddTorque(torque, ForceMode.Impulse);
            }
        }
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