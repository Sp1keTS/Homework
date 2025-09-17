using UnityEngine;

public class CubeScript : MonoBehaviour
{
    public float RotationSpeed {get; set;}
    public Vector3 RotationDirection {get; set;}
    void Update()
    {
        transform.RotateAround(Vector3.zero,RotationDirection, RotationSpeed * Time.deltaTime);
    }
}
