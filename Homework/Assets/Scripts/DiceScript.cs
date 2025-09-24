using System;
using UnityEngine;

public class DiceScript : MonoBehaviour
{
    [SerializeField] private float sideThreshold = 0.9f;
    [SerializeField] private float speedThreshold = 0.1f;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float timer = 0;
    [SerializeField] private float timerMax = 0.5f;
    

    public int CurentSide {get; private set; }
    
    public int GetUpwardSide()
    {
        Vector3[] directions = new Vector3[]
        {
            transform.up,        
            -transform.up,      
            transform.right,     
            -transform.right,   
            transform.forward,   
            -transform.forward   
        };
        
        int[] values = new int[] { 1, 6, 5, 2, 4, 3 };
        for (int i = 0; i < directions.Length; i++)
        {
            if (Vector3.Dot(directions[i], Vector3.up) > sideThreshold)
            {
                return values[i];
            }
        }
        return 1; 
    }

    private void FixedUpdate()
    {
        if (IsStill())
        {
            timer += Time.fixedDeltaTime;
            if (timer >= timerMax)
            {
                timer = 0;
                CurentSide = GetUpwardSide();
            }
        }
        else
        {
            timer = 0;
        }
    }

    public bool IsStill()
    {
        return rb.linearVelocity.magnitude < speedThreshold;
    }
}