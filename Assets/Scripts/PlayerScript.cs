using System;
using System.Collections;
using System.Drawing;
using TMPro;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public PlayerSoundController playerSoundController;
    private Animator animator;
    public bool isWalking = false;
    [SerializeField] private FloatingDamage floatingDamagePrefab;
    [SerializeField] private Canvas targetCanvas; // Arrastra el Canvas aquí en el inspector
    public MenuSystem menuSystem;

    public GameObject NPotion;
    private bool recibiendoDanio = false;
    private int vida;
    private int constitucion;
    private int fuerza;

    private bool muerto = false;
    private bool muerteAnimada = false;
    private bool canAtack = false;
    private bool attack = false;
    private float attackCooldown = 10.0f;
    private float attackTimer = 0.0f;
    private bool firstAttack = true;

    private float tiempoUltimaPulsacion = 0f;
    private float cooldown = 1f; // tiempo en segundos entre pulsaciones permitidas

    void Start()
    {
        //floatingDamagePrefab = GameObject.FindWithTag("Danio");
        //vida inicial = 12 + constitucion
        constitucion = 3;
        vida = 12 + constitucion;
        fuerza = 16;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        string text = NPotion.GetComponent<TextMeshProUGUI>().text;
        int nPotion;
        if (int.TryParse(text, out nPotion))
        {
            // Ahora nPotion contiene el valor entero convertido de text  
        }
        else
        {
            Debug.LogError("El texto no se pudo convertir a un número entero.");
        }

        if (muerto)
        {
            isWalking = false;

            if (!muerteAnimada)
            {
                animator.SetBool("isDead", true);
                muerteAnimada = true;
                //Esperar 2 segundos e ir a la escena de Game Over
                StartCoroutine(EsperarGameOver());

            }

            return;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            isWalking = true;
            animator.SetBool("isStart", isWalking);
            animator.SetBool("isWalking", isWalking);
        }
        if (Input.GetKey(KeyCode.UpArrow) && Time.time - tiempoUltimaPulsacion > cooldown && nPotion>0)
        {
            playerSoundController.PlayBeberPocionSound();
            nPotion--;
            NPotion.GetComponent<TextMeshProUGUI>().text = nPotion.ToString();
            tiempoUltimaPulsacion = Time.time;
            vida += 5;
            Canvas mainCanvas = FindObjectOfType<Canvas>(); // Solo si hay un único Canvas  
            FloatingDamage dmgObj = Instantiate(floatingDamagePrefab, mainCanvas.transform);

            dmgObj.GetComponent<FloatingDamage>().SetText("+" + "5".ToString(), UnityEngine.Color.green);
            dmgObj.SetPosition(transform.position);
            dmgObj.GetComponent<FloatingDamage>().SetVisible();
        }
        if (!animator.GetBool("isStart"))
        {
            isWalking = false;
        }
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
    }
    private IEnumerator EsperarGameOver()
    {
        yield return new WaitForSeconds(2f);
        menuSystem.GameOver();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EspadaEsqueleto"))
        {
            //Vector2 direccionDanio = new Vector2(collision.gameObject.transform.position.x,0);
            //collision.gameObject.GetComponent<EnemyScript>().
            EnemyScript enemyScript = collision.gameObject.GetComponentInParent<EnemyScript>();

            RecibeDanio(enemyScript.getDanio());
        }
    }

    public void RecibeDanio(int cantDanio)
    {
        if (!recibiendoDanio)
        {
            recibiendoDanio = true;
            animator.SetBool("getDmg", recibiendoDanio);

            // Instanciar daño flotante
            float porcentaje = (float)cantDanio / vida;
            UnityEngine.Color color = porcentaje >= 0.2f ? UnityEngine.Color.red : UnityEngine.Color.white;
            //Debug.Log(floatingDamagePrefab);
            Canvas mainCanvas = FindObjectOfType<Canvas>(); // Solo si hay un único Canvas
            FloatingDamage dmgObj = Instantiate(floatingDamagePrefab, mainCanvas.transform);
            //dmgObj.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up);

            dmgObj.GetComponent<FloatingDamage>().SetText("-" + cantDanio.ToString(), color);
            dmgObj.SetPosition(transform.position);
            dmgObj.GetComponent<FloatingDamage>().SetVisible();

            vida -= cantDanio;
            
            if (vida <= 0)
            {
                muerto = true;
                
                // Aquí puedes agregar la lógica para manejar la muerte del jugador
                //Debug.Log("El jugador ha muerto");
            }
            else
            {
                // Aquí puedes agregar la lógica para manejar el daño recibido por el jugador
                //Debug.Log("El jugador ha recibido daño" + cantDanio);
            }
            //vida -= cantDanio;
            //if (vida <= 0)
            //{
            //    // Aquí puedes agregar la lógica para manejar la muerte del jugador
            //    Debug.Log("El jugador ha muerto");
            //}
            //else
            //{
            //    // Aquí puedes agregar la lógica para manejar el daño recibido por el jugador
            //    Debug.Log("El jugador ha recibido daño");
            //}
        }
    }

    public void desacivaDanio()
    {
        recibiendoDanio = false;
        animator.SetBool("getDmg", recibiendoDanio);
        Debug.Log("Desactivando daño");
    }

    public void quedarseQuieto()
    {
        isWalking = false;
        animator.SetBool("isWalking", isWalking);
    }

    public int getVida()
    {
        return vida;
    }

    public int getDanio()
    {
        //1d20 + fuerza 3
        int danioFuerza= (fuerza -10)/2;
        return UnityEngine.Random.Range(1, 12) + danioFuerza;
    }
    public void doAttack()
    {
        attack = true;
    }

    public void pararAtacar()
    {
        attack = false;
        //firstAttack = true;
    }
    public Boolean isAttacking()
    {
        return attack;
    }
    public void moverse()
    {
        isWalking = true;
        animator.SetBool("isWalking", isWalking);
    }
    public bool isMuerto()
    {
        return muerto;
    }
}
