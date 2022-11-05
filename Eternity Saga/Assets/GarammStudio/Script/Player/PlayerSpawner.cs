using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [Header("Game Manager Data"), SerializeField] private GameManagerDataSO gameManager;
    [Header("Gizmos Property"), SerializeField] private Mesh mesh;
    [SerializeField] private Color color = Color.green;

    private void Start()
    {
        var position = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
        gameManager.PlayerPrefab.LoadAssetAsync().Completed += (playerPrefab) =>
        {
            gameManager.PlayerPrefab.InstantiateAsync(position,Quaternion.identity);
            Destroy(gameObject);
        };
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = color;
        Gizmos.DrawWireMesh(mesh,transform.position);
    }
}
