using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    [SerializeField] private string targetScene = "Maze";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //SceneController.instance.LoadLevel(targetScene);
            EventManager.InvokeLoadLevelEvent(targetScene);
        }
    }
}