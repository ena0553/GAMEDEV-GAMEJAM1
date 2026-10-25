using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    [SerializeField] private Animator transition;
    private bool transitioning = false;

    private void Awake()
    {
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !transitioning)
        {
            LoadLevel("Maze");
        }
    }

    public void LoadLevel(string sceneName)
    {
        if (transitioning) return;
        StartCoroutine(LoadLevelRoutine(sceneName));
    }

    private IEnumerator LoadLevelRoutine(string sceneName)
    {
        transitioning = true;

        transition.ResetTrigger("Start");
        transition.SetTrigger("End");

        yield return new WaitForSeconds(1f);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Trigger fade-in for the new scene
        transition.ResetTrigger("End");
        transition.SetTrigger("Start");

        // Wait for the fade-in animation to finish before allowing another transition
        yield return new WaitForSeconds(1f);

        transitioning = false;
    }

    private void Update()
    {
        if (Keyboard.current != null)
        {
            if (Keyboard.current.f1Key.wasPressedThisFrame) LoadLevel("Maze");
            if (Keyboard.current.f2Key.wasPressedThisFrame) LoadLevel("Lobby");
        }
    }
}