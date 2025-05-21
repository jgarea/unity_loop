using UnityEngine;

public class Enviroment : MonoBehaviour
{
    public PlayerScript player;
    public GameObject tree1;
    public GameObject tree2;
    public GameObject tree3;
    public float speed = 2.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //mover el arbol hacia la izquierda mientras se mueve el jugador
        if (player.isWalking)
        {
            Vector3 position = tree1.transform.position;
            position.x -= speed * Time.deltaTime;
            tree1.transform.position = position;
            // Si el árbol sale de la pantalla, lo reposicionamos a la derecha
            if (tree1.transform.position.x < -10)
            {
                Vector3 newPosition = new Vector3(10, tree1.transform.position.y, tree1.transform.position.z);
                tree1.transform.position = newPosition;
            }
            //mover el arbol2 hacia la izquierda mientras se mueve el jugador
            Vector3 position2 = tree2.transform.position;
            position2.x -= speed * Time.deltaTime;
            tree2.transform.position = position2;
            // Si el árbol2 sale de la pantalla, lo reposicionamos a la derecha
            if (tree2.transform.position.x < -10)
            {
                Vector3 newPosition = new Vector3(10, tree2.transform.position.y, tree2.transform.position.z);
                tree2.transform.position = newPosition;
            }
            //mover el arbol3 hacia la izquierda mientras se mueve el jugador
            Vector3 position3 = tree3.transform.position;
            position3.x -= speed * Time.deltaTime;
            tree3.transform.position = position3;
            // Si el árbol3 sale de la pantalla, lo reposicionamos a la derecha
            if (tree3.transform.position.x < -10)
            {
                Vector3 newPosition = new Vector3(10, tree3.transform.position.y, tree3.transform.position.z);
                tree3.transform.position = newPosition;
            }
        }
    }
}
