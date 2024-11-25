using UnityEngine;

public class RotatingObstacle : MonoBehaviour
{
    public float rotationSpeed = 100f;

    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime); // Rotación constante
    }
}
