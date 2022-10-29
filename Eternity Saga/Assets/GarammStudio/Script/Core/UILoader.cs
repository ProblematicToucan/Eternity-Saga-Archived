using UnityEngine;
using UnityEngine.SceneManagement;

public class UILoader : MonoBehaviour
{
    [SerializeField] private bool loadUiScene = true;
    [SerializeField] private bool showFPS = false;
    [field: SerializeField] public SceneLoaderSO SceneLoaderSO { get; private set; }

    void Awake()
    {
        Crossfade();
        if (!loadUiScene) return;
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

    private async void Crossfade()
    {
        await SceneLoaderSO.Crossfade(CrossfadeType.fadeout, 1);
    }

    private void Start()
    {
#if UNITY_EDITOR
        Application.targetFrameRate = 300;
#else
        Application.targetFrameRate = Screen.currentResolution.refreshRate;
#endif
    }

    private void OnGUI()
    {
        if (!showFPS) return;
        GUI.Label(new Rect(100, 10, 100, 100), $"{(int)(1.0f / Time.smoothDeltaTime)}");
    }
}
