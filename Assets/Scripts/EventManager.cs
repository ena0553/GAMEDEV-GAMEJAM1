using UnityEngine.Events;

public static class EventManager
{
    public static event UnityAction PlayerDeathEvent;
    public static event UnityAction<string> LoadLevelEvent;
    public static event UnityAction GoToLobbyEvent;
    public static event UnityAction VictoryEvent;
    public static event UnityAction ReturnToStartEvent;

    public static void InvokePlayerDeathEvent()
    {
        PlayerDeathEvent?.Invoke();
    }

    public static void InvokeLoadLevelEvent(string sceneName)
    {
        LoadLevelEvent?.Invoke(sceneName);
    }

    public static void InvokeGoToLobbyEvent()
    {
        GoToLobbyEvent?.Invoke();
    }

    public static void InvokeVictoryEvent()
    {
        VictoryEvent?.Invoke();
    }

    public static void InvokeReturnToStartEvent()
    {
        ReturnToStartEvent?.Invoke();
    }
}
