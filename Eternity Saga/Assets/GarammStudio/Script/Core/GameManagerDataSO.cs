using UnityEngine;

[CreateAssetMenu(fileName = "Game Manager Data", menuName = "GarammStudio/Core/Game Manager Data")]
public class GameManagerDataSO : ScriptableObject
{
    [field: SerializeField] public GameObject PlayerPrefab { get; private set; }
}
