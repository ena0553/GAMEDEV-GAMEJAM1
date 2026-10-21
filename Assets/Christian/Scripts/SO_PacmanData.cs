using UnityEngine;

[CreateAssetMenu(fileName = "SO_PacmanData", menuName = "ScriptableObjects/SO_PacmanData")]
public class SO_PacmanData : ScriptableObject
{
    public int moveSpeed;
    // note: not actually per frame because we use FixedUpdate (it's just for clarity)
    [Range(0, 1)] public float teleportChancePerFrame;
}