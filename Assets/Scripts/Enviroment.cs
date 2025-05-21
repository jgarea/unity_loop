using UnityEngine;

public class Enviroment : MonoBehaviour
{
    public PlayerScript player;
    public GameObject[] trees; 
    public float speed = 2.0f;

    void Update()
    {
        if (player.isWalking)
        {
            foreach (GameObject tree in trees)
            {
                Vector3 position = tree.transform.position;
                position.x -= speed * Time.deltaTime;
                tree.transform.position = position;

                if (tree.transform.position.x < -10)
                {
                    tree.transform.position = new Vector3(10, position.y, position.z);
                }
            }
        }
    }
}
