
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Replay()
    {
        Debug.Log("Button Clicked!");
        //UnityEngine.SceneManagement.SceneManager.LoadScene("Lobby");
        EventManager.InvokeReturnToStartEvent();
    }
}
