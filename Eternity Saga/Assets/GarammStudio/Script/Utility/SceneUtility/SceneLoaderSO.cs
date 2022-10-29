using System.ComponentModel;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Scene Loader", menuName = "GarammStudio/Utility/Scene Loader")]
public class SceneLoaderSO : ScriptableObject
{
    [field: SerializeField] public GameObject LoadingScreenPrefab { get; private set; }
    [field: SerializeField] public GameObject CrossfadePanel { get; private set; }
    [field: SerializeField, Description("Time to animate crossfade in second"), Range(1, 5)] public float AnimateTime { get; private set; }

    public async void LoadScene(string sceneName)
    {
        await Crossfade(CrossfadeType.fadein, AnimateTime);
        var loadingScreen = Instantiate(LoadingScreenPrefab, Vector3.up, Quaternion.identity);
        var slider = loadingScreen.GetComponentInChildren<Slider>();
        var operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone)
        {
            var progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            await Task.Yield();
        }
    }

    public async Task Crossfade(CrossfadeType type, float duration)
    {
        var panel = Instantiate(CrossfadePanel, Vector3.up, Quaternion.identity).GetComponent<CanvasGroup>();
        float t = 0f;
        if (type == CrossfadeType.fadein)
        {
            panel.alpha = 0;
            while (t < duration)
            {
                panel.alpha = t / duration;
                t += Time.deltaTime;
                await Task.Yield();
            }
            await Task.Delay(500);
        }
        else
        {
            panel.alpha = 1;
            await Task.Delay(1000);
            while (t < duration)
            {
                panel.alpha = 1 - t / duration;
                t += Time.deltaTime;
                await Task.Yield();
            }
        }
    }
}

public enum CrossfadeType
{
    fadein,
    fadeout,
}