using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Scene Loader", menuName = "GaramStudio/Utility/Scene Loader")]
public class SceneLoaderSO : ScriptableObject
{
    [field: SerializeField] public GameObject LoadingBarPrefab { get; private set; }
    public async void LoadScene(int sceneIndex)
    {
        var loadingbar = Instantiate(LoadingBarPrefab, Vector3.up, Quaternion.identity);
        var slider = loadingbar.GetComponentInChildren<Slider>();
        var operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
        {
            var progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            await Task.Yield();
        }
    }
}