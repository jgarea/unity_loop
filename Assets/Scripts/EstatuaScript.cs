using UnityEngine;

public class EstatuaScript : MonoBehaviour
{

    private Vector2 movement;
    private GameObject player;
    private PlayerScript playerScript;
    public float speed = 2.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        movimiento();

    }
    private void movimiento()
    {
        if (playerScript.isWalking)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);

        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}