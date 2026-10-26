
using UnityEngine;

public class StartScreen : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Play()
    {
        Debug.Log("Button Clicked!");
        //UnityEngine.SceneManagement.SceneManager.LoadScene("Lobby");
        EventManager.InvokeGoToLobbyEvent();
    }
}
