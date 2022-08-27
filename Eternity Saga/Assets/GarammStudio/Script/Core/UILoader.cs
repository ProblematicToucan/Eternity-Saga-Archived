using UnityEngine;
using UnityEngine.SceneManagement;

public class UILoader : MonoBehaviour
{
    [SerializeField] private bool showFPS = false;
    void Awake()
    {
        var sceneUI = SceneManager.GetSceneByName("UI");
        if (sceneUI.isLoaded == false)
        {
            SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
        }
        else
        {
            SceneManager.UnloadSceneAsync("UI");
        }
    }

    private void Start()
    {
        Application.targetFrameRate = Screen.currentResolution.refreshRate;
    }

    private void OnGUI()
    {
        if (!showFPS) return;
        GUI.Label(new Rect(100, 100, 100, 100), $"{(int)(1.0f / Time.smoothDeltaTime)}");
    }
}
