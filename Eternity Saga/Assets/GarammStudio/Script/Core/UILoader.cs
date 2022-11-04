using UnityEngine;
using UnityEngine.AddressableAssets;

public class UILoader : MonoBehaviour
{
    [SerializeField] private bool loadUiScene = true;
    [SerializeField] private AssetReference sceneRef;
    //[SerializeField] private bool showFPS = false;
    [field: SerializeField] public SceneLoaderSO SceneLoaderSO { get; private set; }

    void Awake()
    {
        Crossfade();
        if (!loadUiScene) return;
        SceneLoaderSO.LoadSceneAdditive(sceneRef);
    }

    private async void Crossfade()
    {
        await SceneLoaderSO.Crossfade(CrossfadeType.fadeout, SceneLoaderSO.AnimateTime);
    }

    private void Start()
    {
#if UNITY_EDITOR
        Application.targetFrameRate = 300;
#else
        Application.targetFrameRate = Screen.currentResolution.refreshRate;
#endif
    }

    //private void OnGUI()
    //{
    //    if (!showFPS) return;
    //    GUI.Label(new Rect(100, 10, 100, 100), $"{(int)(1.0f / Time.smoothDeltaTime)}");
    //}
}
