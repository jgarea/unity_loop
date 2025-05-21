using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
    public float speed = 2.0f;
    public float stoppingDistance = 1.5f;
    private bool recibiendoDanio = false;
    private GameObject player;
    private PlayerScript playerScript;
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 movement;
    private bool canAtack = false;
    private bool attack = false;
    private bool isStart = false;
    private float attackCooldown = 5.0f;
    private float attackTimer = 0.0f;
    private bool firstAttack = true;
    private int vida;
    [SerializeField] private FloatingDamage floatingDamagePrefab;
    private bool muerto = false;
    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");
        
        vida = 50;
        playerScript = player.GetComponent<PlayerScript>();
        
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.isMuerto())
        {
            return;
        }
        if (muerto)
        {
            GetComponent<Collider2D>().enabled = false;
            playerScript.pararAtacar();
            playerScript.moverse();
            Destroy(gameObject, 1.0f);
            
            return;
        }
        isStart = playerScript.isWalking;
        if (canAtack)
        {
            playerScript.quedarseQuieto();
        }

        movimiento();
        if (canAtack && firstAttack)
        {
            doAttack();
            firstAttack = false;
        }
        else if (canAtack && !attack)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackCooldown)
            {
                doAttack();
                attackTimer = 0.0f;
            }
            
        }

        animator.SetBool("atacar", attack);
        // Gesti�n del ataque
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerScript.isMuerto())
        {
            return;
        }
        //Debug.Log("Colision con " + collision.gameObject.name);
        if (collision.CompareTag("EspadaHeroe"))
        {
            //Vector2 direccionDanio = new Vector2(collision.gameObject.transform.position.x,0);
            //collision.gameObject.GetComponent<EnemyScript>().

            PlayerScript playerScript = collision.gameObject.GetComponentInParent<PlayerScript>();
            if (muerto)
            {
                Debug.Log("Muerto" );
                playerScript.pararAtacar();
                return;

            }
            if (playerScript.isAttacking()) {
                Debug.Log("Recibe danio");
                RecibeDanio(playerScript.getDanio());
            }
            else
            {
                playerScript.doAttack();
            }
            
            
        }
    }


    private void movimiento()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance > stoppingDistance)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            movement = direction * speed;
            canAtack = false;
        }
        else
        {
            canAtack = true;
            movement = Vector2.zero;
        }
        if (isStart)
        {
            rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
        }
    }

    public void RecibeDanio(int cantDanio)
    {
        Debug.Log("Recibe danio" + cantDanio);
        //if (!recibiendoDanio)
        //{
        //    recibiendoDanio = true;
        //    animator.SetBool("getDmg", recibiendoDanio);

        // Instanciar da�o flotante
        float porcentaje = (float)cantDanio / vida;
        Color color = porcentaje >= 0.2f ? Color.red : Color.white;
        //Debug.Log(floatingDamagePrefab);
        Canvas mainCanvas = FindObjectOfType<Canvas>(); // Solo si hay un �nico Canvas
        FloatingDamage dmgObj = Instantiate(floatingDamagePrefab, mainCanvas.transform);
        //dmgObj.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up);

        dmgObj.GetComponent<FloatingDamage>().SetText("-" + cantDanio.ToString(), color);
        dmgObj.SetPosition(transform.position);
        dmgObj.GetComponent<FloatingDamage>().SetVisible();

        vida -= cantDanio;

            if (vida <= 0)
            {
                muerto = true;

                // Aqu� puedes agregar la l�gica para manejar la muerte del jugador
                Debug.Log("El enemigo ha muerto");
            }
            else
            {
                // Aqu� puedes agregar la l�gica para manejar el da�o recibido por el jugador
                Debug.Log("El enemigo ha recibido da�o" + cantDanio);
            }
            //vida -= cantDanio; b
            //if (vida <= 0)
            //{
            //    // Aqu� puedes agregar la l�gica para manejar la muerte del jugador
            //    Debug.Log("El jugador ha muerto");
            //}
            //else
            //{
            //    // Aqu� puedes agregar la l�gica para manejar el da�o recibido por el jugador
            //    Debug.Log("El jugador ha recibido da�o");
            //}
        //}
    }

    public void doAttack()
    {
        attack = true;
    }

    public int getDanio()
    {
        // calculo del da�o 1d6
        int dado = Random.Range(1, 7);
        return dado;
    }

    public void stopAttack()
    {
        attack = false;
    }

    public void desacivaDanio()
    {
        recibiendoDanio = false;
        animator.SetBool("getDmg", recibiendoDanio);
        Debug.Log("Desactivando da�o");
    }

}
