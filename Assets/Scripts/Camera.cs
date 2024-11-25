using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    private Vector3 offset;  // Desfase entre la cámara y el jugador

    void Start()
    {
        // Calcula el desfase inicial
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // Sigue la posición del jugador pero no rota con él
        transform.position = player.position + offset;
        transform.rotation = Quaternion.Euler(90f, 0f, 0f); // Vista cenital fija
    }
}
