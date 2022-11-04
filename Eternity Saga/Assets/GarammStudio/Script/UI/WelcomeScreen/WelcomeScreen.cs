using UnityEngine;
using UnityEngine.AddressableAssets;

public class WelcomeScreen : MonoBehaviour
{
    [SerializeField] private AssetReference assetRef;
    [SerializeField] private SceneLoaderSO sceneLoaderSO;

    public void Login()
    {
        sceneLoaderSO.LoadScene(assetRef);
    }
}
