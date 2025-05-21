using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public GameObject prefabToSpawn2;
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

                // Cada 2 spawns, instanciar el segundo prefab
                spawnCount++;
                if (spawnCount % 2 == 0)
                {
                    SpawnObject2();
                }
            }
        }
        else
        {
            // Si el jugador no se mueve, no suma tiempo
            
        }
    }

    private int spawnCount = 0;

    public void SpawnObject2()
    {
        Quaternion rotation = Quaternion.Euler(0, 0, 0);
        //Instantiate(prefabToSpawn2, spawnPoint[Random.Range(0, spawnPoint.Length)].position, rotation);
        Instantiate(prefabToSpawn2, spawnPoint[1].position, rotation);
    }

    public void SpawnObject()
    {
        // Fix: Use Quaternion.Euler to set the rotation
        Quaternion rotation = Quaternion.Euler(0, 180, 0);
        //Instantiate(prefabToSpawn, spawnPoint[Random.Range(0, spawnPoint.Length)].position, rotation);
        Instantiate(prefabToSpawn, spawnPoint[0].position, rotation);
    }
}
