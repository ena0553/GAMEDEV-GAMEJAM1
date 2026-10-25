using UnityEngine;

public class Player : MonoBehaviour
{
    private void OnEnable()
    {
        ObjectRegistry.player = this;
    }

    private void OnDisable()
    {
        ObjectRegistry.player = null;
    }
}
