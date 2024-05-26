using UnityEngine;
using UnityEngine.AI;

public class NPCBehavior : MonoBehaviour
{
    public Transform player;  // Referencia al jugador
    public float fleeDistance = 5.0f;  // Distancia a la que el NPC empezará a huir
    public float fleeSpeed = 3.5f;  // Velocidad del NPC al huir
    public float wanderRadius = 10.0f;  // Radio para el movimiento aleatorio
    public float wanderTimer = 5.0f;  // Tiempo entre movimientos aleatorios

    private NavMeshAgent agent;  // NavMeshAgent del NPC
    private float timer;  // Temporizador para el movimiento aleatorio
    private Vector3 lastWanderDestination;  // Última dirección aleatoria para evitar oscilaciones
    private bool isColliding;  // Indicador de colisión

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = fleeSpeed;  // Establece la velocidad del NavMeshAgent
        agent.angularSpeed = 120f; // Ajusta la velocidad de giro para maniobrar mejor
        agent.stoppingDistance = 0.5f; // Ajusta la distancia de parada para evitar quedar muy cerca de las paredes
        timer = wanderTimer;
        isColliding = false;
        Debug.Log("NavMeshAgent configurado.");
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        Debug.Log("Distancia al jugador: " + distanceToPlayer);

        if (distanceToPlayer < fleeDistance)
        {
            Debug.Log("Jugador dentro del rango, NPC huirá.");
            FleeFromPlayer();
        }
        else
        {
            timer += Time.deltaTime;

            if (timer >= wanderTimer && !isColliding)
            {
                Wander();
                timer = 0;
            }
        }

        // Detectar obstáculos y ajustar dirección si es necesario
        DetectObstacles();
    }

    void FleeFromPlayer()
    {
        Vector3 dirToPlayer = transform.position - player.position;
        Vector3 fleeDirection = transform.position + dirToPlayer.normalized * fleeDistance;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(fleeDirection, out hit, 1.0f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
            Debug.Log("Nueva posición de huida: " + hit.position);
        }
        else
        {
            Debug.Log("No se encontró una posición válida en el NavMesh.");
        }
    }

    void Wander()
    {
        Vector3 wanderDirection = Random.insideUnitSphere * wanderRadius;
        wanderDirection += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(wanderDirection, out hit, wanderRadius, NavMesh.AllAreas))
        {
            lastWanderDestination = hit.position;
            agent.SetDestination(hit.position);
            Debug.Log("Nueva posición aleatoria: " + hit.position);
        }
        else
        {
            Debug.Log("No se encontró una posición válida en el NavMesh.");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall")) // Asegúrate de que las paredes tengan el tag "Wall"
        {
            Debug.Log("Colisión con pared detectada.");
            isColliding = true;
            Vector3 awayFromWall = transform.position - collision.contacts[0].point;
            Vector3 newDirection = transform.position + awayFromWall.normalized * wanderRadius;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(newDirection, out hit, wanderRadius, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
                Debug.Log("Nueva dirección para evitar pared: " + hit.position);
            }
            else
            {
                Debug.Log("No se encontró una posición válida en el NavMesh para evitar pared.");
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isColliding = false;
        }
    }

    void DetectObstacles()
    {
        // Realizar un raycast hacia adelante para detectar obstáculos
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1.0f))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                Debug.Log("Obstáculo detectado, ajustando dirección.");
                Vector3 avoidDirection = Vector3.Cross(transform.forward, Vector3.up).normalized; // Cambia la dirección para evitar el obstáculo
                Vector3 newDirection = transform.position + avoidDirection * wanderRadius;

                NavMeshHit navHit;
                if (NavMesh.SamplePosition(newDirection, out navHit, wanderRadius, NavMesh.AllAreas))
                {
                    agent.SetDestination(navHit.position);
                    Debug.Log("Nueva dirección para evitar obstáculo: " + navHit.position);
                }
            }
        }
    }
}
