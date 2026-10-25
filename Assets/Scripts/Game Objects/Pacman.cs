using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(NavMeshAgent))]
public class Pacman : MonoBehaviour
{
    [SerializeField] private SO_PacmanData pacmanData;
    private NavMeshAgent navMeshAgent;
    private Coroutine pathToPlayerCoroutine;
    [SerializeField] private Transform pacmanTeleportPoint; 
    [SerializeField] private float deathRespawnDelay = 1f;
    private Coroutine teleportAfterDeathCoroutine;

    private void OnEnable()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (pathToPlayerCoroutine != null)
        {
            StopCoroutine(pathToPlayerCoroutine);
        }
        pathToPlayerCoroutine = StartCoroutine(GoToPlayerRoutine());
        TeleportToStartingPoint();
    }

    private void OnDisable()
    {
        if (pathToPlayerCoroutine != null)
        {
            StopCoroutine(pathToPlayerCoroutine);
        }
        pathToPlayerCoroutine = null;
    }

    private void FixedUpdate()
    {
        float x = Random.value;
        float y = pacmanData.teleportChancePerFrame;
        if (Random.value < pacmanData.teleportChancePerFrame)
        {
            int randomIndex = Random.Range(0, ObjectRegistry.pellets.Count);
            Pellet randomPellet = ObjectRegistry.pellets[randomIndex];
            TeleportToPellet(randomPellet);
        }
        GoToPlayer();
    }

    private void TeleportToPellet(Pellet pellet)
    {
        navMeshAgent.enabled = false; // so the agent doesn't mess with the teleport
        transform.SetPositionAndRotation(pellet.transform.position, pellet.pacmanRotation);
        navMeshAgent.enabled = true;
    }

    private void GoToPlayer()
    {
        if (ObjectRegistry.player == null) return;
        navMeshAgent.SetDestination(ObjectRegistry.player.transform.position);
    }

    private IEnumerator GoToPlayerRoutine()
    {
        GoToPlayer();
        yield return new WaitForSeconds(pacmanData.timeBetweenPathingToPlayer);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventManager.InvokePlayerDeathEvent();

            if (teleportAfterDeathCoroutine != null)
            {
                StopCoroutine(teleportAfterDeathCoroutine);
            }
            teleportAfterDeathCoroutine = StartCoroutine(TeleportAfterDeathRoutine());
        }
    }

    private IEnumerator TeleportAfterDeathRoutine()
    {
        yield return new WaitForSeconds(deathRespawnDelay);
        TeleportToStartingPoint();
        teleportAfterDeathCoroutine = null;
    }

    private void TeleportToStartingPoint()
    {
        if (pacmanTeleportPoint != null)
        {
            navMeshAgent.enabled = false;
            transform.SetPositionAndRotation(pacmanTeleportPoint.position, pacmanTeleportPoint.rotation);
            navMeshAgent.enabled = true;
        }
    }
}
