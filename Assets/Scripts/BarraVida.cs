using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Image barraVida;
    private PlayerScript playerScript;
    private int vidaMaxima;
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
        vidaMaxima = playerScript.getVida();
        barraVida = GameObject.FindWithTag("Fill").GetComponent<Image>();
    }

        // Update is called once per frame
        void Update()
    {
        if (vidaMaxima == 0)
        {
            vidaMaxima = playerScript.getVida();
        }
        barraVida.fillAmount = (float) playerScript.getVida() / vidaMaxima;
        //Debug.Log("Vida: " + playerScript.getVida() + "vidaMaxima" + vidaMaxima + " fill " + barraVida.fillAmount);
    }
}
