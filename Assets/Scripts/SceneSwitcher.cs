using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public static SceneSwitcher instance;

    private void Awake()
    {
        Transform parentTransform = transform.parent;

        if (instance == null)
        {
            instance = this;
            transform.SetParent(null); // Ensure it is a root object
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (parentTransform != null && parentTransform.childCount == 0)
        {
            Destroy(parentTransform.gameObject);
        }
    }

    private void OnEnable()
    {
        EventManager.LoadLevelEvent += SwitchToLevel;
        EventManager.GoToLobbyEvent += SwitchToLobby;
        EventManager.VictoryEvent += SwitchToVictoryScreen;
        EventManager.ReturnToStartEvent += SwitchToStartScreen;
    }

    private void OnDisable()
    {
        EventManager.LoadLevelEvent -= SwitchToLevel;
        EventManager.GoToLobbyEvent -= SwitchToLobby;
        EventManager.VictoryEvent -= SwitchToVictoryScreen;
        EventManager.ReturnToStartEvent -= SwitchToStartScreen;
    }

    private void SwitchToLevel(string targetScene)
    {
        SceneController.instance.LoadLevel(targetScene);
    }

    private void SwitchToLobby()
    {
        SceneManager.LoadScene("Lobby");
    }

    private void SwitchToVictoryScreen()
    {
        SceneManager.LoadScene("VictoryScene");
    }

    private void SwitchToStartScreen()
    {
        SceneManager.LoadScene("StartScreen");
    }
}
