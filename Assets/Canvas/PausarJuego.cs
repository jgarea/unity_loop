using UnityEngine;

public class PausarJuego : MonoBehaviour
{
    public GameObject menuPausa;
    public bool juegoPausado = false;

// Start is called once before the first execution of Update after the MonoBehaviour is created
void Start()
    {

        reanudarJuego(); // Asegúrate de que el juego no esté pausado al inicio
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPausado)
            {
                reanudarJuego();
            }
            else
            {
                pausarJuego();
            }
        }
    }
    public void pausarJuego()
    {
        menuPausa.SetActive(true);
        Time.timeScale = 0f; // Detiene el tiempo del juego
        juegoPausado = true;
    }

    public void reanudarJuego()
    {
        menuPausa.SetActive(false);
        Time.timeScale = 1f; // Reanuda el tiempo del juego
        juegoPausado = false;
    }

}