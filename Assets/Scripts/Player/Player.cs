using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;

    private Vector3 startPos;
    private bool dying;

    private void Awake()
    {
        startPos = transform.position;
    }

    private void OnEnable()
    {
        ObjectRegistry.player = this;
        EventManager.PlayerDeathEvent += Die;
    }

    private void OnDisable()
    {
        ObjectRegistry.player = null;
        EventManager.PlayerDeathEvent -= Die;
    }
    
    public void Die()
    {
        if (dying){ 
            return; 
        }

        StartCoroutine(DieRoutine());
    }

    private IEnumerator DieRoutine()
    {
        dying = true;

        var controller = GetComponent<CharacterController>();
        if (controller != null) controller.enabled = false;

        var rb = GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        GetComponent<Movement>().enabled = false;

        foreach (var r in GetComponentsInChildren<Renderer>())
            r.enabled = false;

        yield return new WaitForSeconds(1);

        transform.position = startPos;
        foreach (var r in GetComponentsInChildren<Renderer>())
            r.enabled = true;

        if (controller != null) controller.enabled = true;
        if (rb != null) rb.isKinematic = false;
        GetComponent<Movement>().enabled = true;

        dying = false;
    }


}

