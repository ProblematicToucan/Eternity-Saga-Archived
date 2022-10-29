using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [Header("Game Manager Data"), SerializeField] private GameManagerDataSO gameManager;
    [Header("Gizmos Property"), SerializeField] private Mesh mesh;
    [SerializeField] private Color color = Color.green;

    private void Start()
    {
        Instantiate(gameManager.PlayerPrefab);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawWireMesh(mesh);
    }
}
