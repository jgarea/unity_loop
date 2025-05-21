using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuSystem : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("Principal");
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
}
