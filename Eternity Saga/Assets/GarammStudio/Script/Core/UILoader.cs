using UnityEngine;
using UnityEngine.SceneManagement;

public class UILoader : MonoBehaviour
{
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
}
