using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(NavMeshAgent))]
public class Pacman : MonoBehaviour
{
    [SerializeField] private SO_PacmanData pacmanData;
    private NavMeshAgent navMeshAgent;
    private Coroutine pathToPlayerCoroutine;

    private void OnEnable()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (pathToPlayerCoroutine != null)
        {
            StopCoroutine(pathToPlayerCoroutine);
        }
        pathToPlayerCoroutine = StartCoroutine(GoToPlayerEnumerator());
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

    private IEnumerator GoToPlayerEnumerator()
    {
        GoToPlayer();
        yield return new WaitForSeconds(pacmanData.timeBetweenPathingToPlayer);
    }
}
