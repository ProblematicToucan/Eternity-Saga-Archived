using System.ComponentModel;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Scene Loader", menuName = "GarammStudio/Utility/Scene Loader")]
public class SceneLoaderSO : ScriptableObject
{
    [field: SerializeField] public GameObject LoadingScreenPrefab { get; private set; }
    [field: SerializeField] public GameObject CrossfadePanel { get; private set; }
    [field: SerializeField, Description("Time to animate crossfade in second"), Range(1, 5)] public float AnimateTime { get; private set; }
    [field: SerializeField] public AnimationCurve AnimateCurve { get; private set; } = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    public async void LoadScene(AssetReference sceneRef)
    {
        await Crossfade(CrossfadeType.fadein, AnimateTime);
        var loadingScreen = Instantiate(LoadingScreenPrefab, Vector3.up, Quaternion.identity);
        var slider = loadingScreen.GetComponentInChildren<Slider>();

        var scene = sceneRef.LoadSceneAsync(LoadSceneMode.Single);
        while (!scene.IsDone)
        {
            slider.value = scene.PercentComplete / 0.9f;
            await Task.Yield();
        }

        await Task.Yield();
    }

    public async void LoadSceneAdditive(AssetReference sceneRef)
    {
        var scene = Addressables.LoadSceneAsync(sceneRef, LoadSceneMode.Additive);

        scene.Completed += (operation) =>
        {
            if (operation.Status == AsyncOperationStatus.Succeeded)
            {
                //previousScene = operation.Result;
            }
        };
        await Task.Yield();
    }

    public async Task Crossfade(CrossfadeType type, float duration)
    {
        var canvasGroup = Instantiate(CrossfadePanel, Vector3.up, Quaternion.identity).GetComponent<CanvasGroup>();
        var startTime = Time.time;
        var endTime = Time.time + duration;

        if (type == CrossfadeType.fadein) canvasGroup.alpha = AnimateCurve.Evaluate(0);
        else canvasGroup.alpha = AnimateCurve.Evaluate(1);

        while (Time.time <= endTime)
        {
            float elapsedTime = Time.time - startTime;
            var percentage = 1 / (duration / elapsedTime);

            if (type == CrossfadeType.fadein) canvasGroup.alpha = AnimateCurve.Evaluate(percentage);
            else canvasGroup.alpha = AnimateCurve.Evaluate(1 - percentage);

            await Task.Yield();
        }

        if (type == CrossfadeType.fadein) canvasGroup.alpha = AnimateCurve.Evaluate(1);
        else canvasGroup.alpha = AnimateCurve.Evaluate(0);
    }
}

public enum CrossfadeType
{
    fadein,
    fadeout,
}