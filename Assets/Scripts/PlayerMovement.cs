using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform[] waypoints; // Lista de puntos de referencia
    private Transform currentWaypoint; // Punto actual al que se dirige

    public float waypointThreshold = 1f; // Distancia mínima para considerar que se llegó al punto
    public float dodgeDistance = 3f; // Distancia para esquivar objetos
    public LayerMask fallingObjectLayer; // Capa para detectar objetos que caen

    void Start()
    {
        // Inicializa el agente
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("No se encontró un componente NavMeshAgent en el jugador.");
            return;
        }

        // Asegura que el jugador comience dentro del NavMesh
        if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
        {
            transform.position = hit.position;
            Debug.Log("El jugador está en una posición válida del NavMesh.");
        }
        else
        {
            Debug.LogError("El jugador sigue estando fuera del NavMesh.");
        }

        // Selecciona un punto aleatorio inicial
        SelectRandomWaypoint();
    }

    void Update()
    {
        if (agent == null || waypoints.Length == 0)
            return;

        // Comprueba si el jugador está cerca del punto actual
        if (currentWaypoint != null && Vector3.Distance(transform.position, currentWaypoint.position) < waypointThreshold)
        {
            Debug.Log("Llegó al waypoint: " + currentWaypoint.name);
            SelectRandomWaypoint();
        }

        // Detección de objetos que caen
        DetectAndDodge();
    }

    // Selecciona un punto aleatorio de la lista
    private void SelectRandomWaypoint()
    {
        if (waypoints.Length == 0)
        {
            Debug.LogWarning("No se encontraron waypoints.");
            return;
        }

        // Selecciona un waypoint aleatorio
        currentWaypoint = waypoints[Random.Range(0, waypoints.Length)];
        Debug.Log("Nuevo waypoint seleccionado: " + currentWaypoint.name);

        // Configura el destino del agente
        agent.SetDestination(currentWaypoint.position);
    }

    // Detectar objetos que caen y esquivarlos
    private void DetectAndDodge()
    {
        Collider[] fallingObjects = Physics.OverlapSphere(transform.position, dodgeDistance, fallingObjectLayer);

        if (fallingObjects.Length > 0)
        {
            Debug.Log("¡Objeto detectado! Esquivando...");

            // Cambiar dirección para esquivar
            Vector3 dodgeDirection = Vector3.zero;

            foreach (var obj in fallingObjects)
            {
                // Calcula la dirección opuesta al objeto detectado
                dodgeDirection += (transform.position - obj.transform.position).normalized;
            }

            // Aplica la dirección de esquiva al agente
            dodgeDirection = dodgeDirection.normalized;
            Vector3 dodgePosition = transform.position + dodgeDirection * dodgeDistance;

            // Verifica si la posición de esquiva está en el NavMesh
            if (NavMesh.SamplePosition(dodgePosition, out NavMeshHit navHit, 1.0f, NavMesh.AllAreas))
            {
                agent.SetDestination(navHit.position);
                Debug.Log("Esquiva hacia: " + navHit.position);
            }
        }
    }
}
