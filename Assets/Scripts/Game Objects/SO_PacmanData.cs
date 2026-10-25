using UnityEngine;

[CreateAssetMenu(fileName = "SO_PacmanData", menuName = "ScriptableObjects/SO_PacmanData")]
public class SO_PacmanData : ScriptableObject
{
    [Tooltip("Note: Not actually per frame because we use FixedUpdate (it's just for clarity)")]
    [Range(0, 1)] public float teleportChancePerFrame;

    [Tooltip("Every x seconds, Pac-Man starts a new path going to the player")]
    [Min(0.01f)] public float timeBetweenPathingToPlayer;
}