using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public enum PowerUpType
    {
        AumentarPocionesMaximas,
        AumentarFuerza,
        Regeneracion,
        AumentarConstitucion
    }
    [SerializeField] private FloatingDamage floatingDamagePrefab;
    private PlayerScript player; 
    private HashSet<PowerUpType> powerUpsAplicados = new HashSet<PowerUpType>();
    private string mensajePowerUp = "";

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        //AplicarPowerUpAleatorio();
    }

    public void AplicarPowerUpAleatorio()
    {
        PowerUpType powerUp = (PowerUpType)Random.Range(0, System.Enum.GetValues(typeof(PowerUpType)).Length);

        switch (powerUp)
        {
            case PowerUpType.AumentarPocionesMaximas:
                if (powerUpsAplicados.Contains(PowerUpType.AumentarPocionesMaximas))
                {
                    Debug.Log("Ya se ha aplicado este power-up.");
                    return;
                }
                player.nPotionMax += 1;
                Debug.Log("¡Power-up! Máximas pociones aumentadas en 1.");
                mensajePowerUp = "MAX Potion!";
                powerUpsAplicados.Add(PowerUpType.AumentarPocionesMaximas);
                break;

            case PowerUpType.AumentarFuerza:
                if (powerUpsAplicados.Contains(PowerUpType.AumentarFuerza))
                {
                    Debug.Log("Ya se ha aplicado este power-up.");
                    return;
                }
                player.fuerza += 2;
                Debug.Log("¡Power-up! Fuerza aumentada en 2.");
                mensajePowerUp = "Str++";
                powerUpsAplicados.Add(PowerUpType.AumentarFuerza);
                break;

            case PowerUpType.Regeneracion:
                if (powerUpsAplicados.Contains(PowerUpType.Regeneracion))
                {
                    Debug.Log("Ya se ha aplicado este power-up.");
                    return;
                }
                player.ActivarRegeneracion();
                Debug.Log("¡Power-up! Regeneración activada.");
                mensajePowerUp = "Regen UP";
                powerUpsAplicados.Add(PowerUpType.Regeneracion);
                break;

            case PowerUpType.AumentarConstitucion:
                if (powerUpsAplicados.Contains(PowerUpType.AumentarConstitucion))
                {
                    Debug.Log("Ya se ha aplicado este power-up.");
                    return;
                }
                player.aumentarConstitucion();
                Debug.Log("¡Power-up! Constitución aumentada en 1.");
                mensajePowerUp = "Const++";
                powerUpsAplicados.Add(PowerUpType.AumentarConstitucion);
                break;
        }
        player.mostrarMensaje(mensajePowerUp);
    }
}
