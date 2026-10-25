using System;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    public enum Direction
    {
        North,
        East,
        West,
        South
    }

    [Tooltip("The direction for Pac-Man to face after teleporting to this pellet.")]
    public Direction directionForPacmanToFace;

    [NonSerialized]
    public Quaternion pacmanRotation;

    private void Awake()
    {
        pacmanRotation = directionForPacmanToFace switch
        {
            Direction.North => Quaternion.LookRotation(Vector3.forward),
            Direction.East => Quaternion.LookRotation(Vector3.right),
            Direction.West => Quaternion.LookRotation(Vector3.left),
            Direction.South => Quaternion.LookRotation(Vector3.back),
            _ => throw new Exception("Pellet has invalid direction for Pac-Man to face")
        };
    }

    private void OnEnable()
    {
        ObjectRegistry.pellets.Add(this);
    }

    private void OnDisable()
    {
        ObjectRegistry.pellets.Remove(this);
    }
}
