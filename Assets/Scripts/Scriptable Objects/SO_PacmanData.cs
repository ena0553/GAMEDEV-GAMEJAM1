using UnityEngine;

[CreateAssetMenu(fileName = "SO_PacmanData", menuName = "ScriptableObjects/SO_PacmanData")]
public class SO_PacmanData : ScriptableObject
{
    [Tooltip("Note: Not actually per frame because we use FixedUpdate (it's just for clarity)")]
    [Range(0, 1)] public float teleportChancePerFrame = 0.0005f;

    [Tooltip("Every x seconds, Pac-Man starts a new path going to the player")]
    [Min(0.01f)] public float timeBetweenPathingToPlayer = 0.2f;

    [Tooltip("After the player dies, Pac-Man teleports to the starting position after x seconds")]
    [Min(0f)] public float deathRespawnDelay = 1f;
}