using UnityEngine;
using UnityEngine.SceneManagement;

public class UILoader : MonoBehaviour
{
    void Awake()
    {
        Application.targetFrameRate = 300;
        SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
    }
}
