using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public Transform[] spawnPoint;
    public float spawnInterval = 8f;
    public PlayerScript playerScript;
    private float movingTime = 0f;

    void Update()
    {
        // Solo cuenta el tiempo cuando el jugador se está moviendo
        if (playerScript != null && playerScript.isWalking)
        {
            movingTime += Time.deltaTime;
            if (movingTime >= spawnInterval)
            {
                SpawnObject();
                movingTime = 0f;
            }
        }
        else
        {
            // Si el jugador no se mueve, no suma tiempo
            // (opcional: puedes pausar o resetear movingTime aquí si lo deseas)
        }
    }

    public void SpawnObject()
    {
        // Fix: Use Quaternion.Euler to set the rotation
        Quaternion rotation = Quaternion.Euler(0, 180, 0);
        Instantiate(prefabToSpawn, spawnPoint[Random.Range(0, spawnPoint.Length)].position, rotation);
    }
}
