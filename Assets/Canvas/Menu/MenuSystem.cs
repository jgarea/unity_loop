using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuSystem : MonoBehaviour
{
    public void Jugar()
    {
        StartCoroutine(CargarEscenaConRetraso("Principal", 0.7f));
    }

    public void Salir()
    {
        Application.Quit();
        Debug.Log("Salir");
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    private IEnumerator CargarEscenaConRetraso(string nombreEscena, float segundos)
    {
        yield return new WaitForSeconds(segundos);
        SceneManager.LoadScene(nombreEscena);
    }
}
