using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Para controlar elementos de UI

public class PlayerScore : MonoBehaviour
{
    public int score = 100; // Puntuación inicial
    public Text scoreText; // Referencia al texto UI
    private bool isInvulnerable = false; // Controla la invulnerabilidad
    public float invulnerabilityDuration = 1f; // Duración de invulnerabilidad en segundos

    void Start()
    {
        // Actualiza el texto al inicio
        UpdateScoreText();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Comprueba si el jugador choca con un obstáculo
        if (collision.gameObject.CompareTag("Obstacle") && !isInvulnerable)
        {
            StartCoroutine(HandleCollision());
        }
    }

    IEnumerator HandleCollision()
    {
        Renderer renderer = GetComponent<Renderer>(); // Obtén el renderer del jugador
        Color originalColor = renderer.material.color; // Guarda el color original

        // Resta puntos al chocar
        score -= 10;
        UpdateScoreText();

        // Activa la invulnerabilidad
        isInvulnerable = true;

        // Espera durante el período de invulnerabilidad
        yield return new WaitForSeconds(invulnerabilityDuration);

        // Cambia el color a rojo (u otro) durante la invulnerabilidad
        renderer.material.color = Color.red;

        yield return new WaitForSeconds(invulnerabilityDuration);

        // Desactiva la invulnerabilidad
        isInvulnerable = false;

        // Restaura el color original
        renderer.material.color = originalColor;

    }

    void UpdateScoreText()
    {
        // Actualiza el texto de la puntuación
        scoreText.text = "Puntuación: " + score;
    }
}
