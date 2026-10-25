using UnityEngine.Events;

public static class EventManager
{
    public static event UnityAction PlayerDeathEvent;
    public static event UnityAction<string> LoadLevelEvent;
    public static event UnityAction ReturnToLobbyEvent;
    public static event UnityAction VictoryEvent;

    public static void InvokePlayerDeathEvent()
    {
        PlayerDeathEvent?.Invoke();
    }

    public static void InvokeLoadLevelEvent(string sceneName)
    {
        LoadLevelEvent?.Invoke(sceneName);
    }

    public static void InvokeReturnToLobbyEvent()
    {
        ReturnToLobbyEvent?.Invoke();
    }

    public static void InvokeVictoryEvent()
    {
        VictoryEvent?.Invoke();
    }
}
