using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "Game Manager Data", menuName = "GarammStudio/Core/Game Manager Data")]
public class GameManagerDataSO : ScriptableObject
{
    [field: SerializeField] public AssetReferenceGameObject PlayerPrefab { get; private set; }
}
