using UnityEngine;

public class FallingObjects : MonoBehaviour
{
    public GameObject fallingObject; // Prefab del objeto que cae
    public float spawnInterval = 2f; // Tiempo entre caídas
    public float spawnHeight = 10f; // Altura de generación
    public Vector3 spawnArea; // Área de generación (x, z)

    void Start()
    {
        InvokeRepeating(nameof(SpawnObject), 0f, spawnInterval); // Llama a SpawnObject cada cierto tiempo
    }

    void SpawnObject()
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(-spawnArea.x, spawnArea.x),
            spawnHeight,
            Random.Range(-spawnArea.z, spawnArea.z)
        );

        Instantiate(fallingObject, spawnPosition, Quaternion.identity); // Crea el objeto
    }
}
