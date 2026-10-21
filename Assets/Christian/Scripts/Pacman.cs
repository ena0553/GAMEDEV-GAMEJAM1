using UnityEngine;
using Random = UnityEngine.Random;

public class Pacman : MonoBehaviour
{
    [SerializeField] private SO_PacmanData pacmanData;

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
    }

    private void TeleportToPellet(Pellet pellet)
    {
        transform.SetPositionAndRotation(pellet.transform.position, pellet.pacmanRotation);
    }
}
