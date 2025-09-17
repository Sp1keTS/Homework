
using System;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    [SerializeField][Range(0,10)] int cubeCount;
    [SerializeField] float radius;
    [SerializeField] float rotationSpeed;
    [SerializeField] GameObject cubePrefab;
    [SerializeField] bool clockwise = true;
    
    private CubeScript[] cubes;

    void Awake()
    {
        cubes = new CubeScript[cubeCount];
        for (int i = 0; i < cubeCount; i++)
        {
            GameObject cube = Instantiate(cubePrefab);
            cube.transform.name = $"Cube {i}";

            CubeScript cubeScript = cube.GetComponent<CubeScript>();
            if (cubeScript != null)
            {
                cubeScript.RotationDirection = clockwise ? Vector3.up : Vector3.down;
                cubeScript.RotationSpeed = rotationSpeed;
                cubes[i] = cubeScript;
            }

            float angle = i * (360f / cubeCount) * Mathf.Deg2Rad;
            Vector3 cubePos = new Vector3(
                (float)Math.Cos(angle) * radius,
                0.5f,
                (float)Math.Sin(angle) * radius);
            cube.transform.position = cubePos;
            cube.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
