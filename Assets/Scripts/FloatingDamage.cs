using UnityEngine;
using TMPro;

public class FloatingDamage : MonoBehaviour
{
    public TextMeshProUGUI damageText;
    public float floatSpeed = 1f;
    public float duration = 2f;
    private RectTransform rectTransform;
    private Vector3 worldPosition;

    void Start()
    {
        rectTransform = damageText.GetComponent<RectTransform>();
        Destroy(gameObject, duration);
        //damageText.gameObject.SetActive(false);
    }

    void Update()
    {
        transform.Translate(Vector3.up * floatSpeed * Time.deltaTime);
    }

    public void SetText(string text, Color color)
    {
        damageText.text = text;
        damageText.color = color;
    }
    public void SetVisible()
    {
        //damageText.gameObject.SetActive(true);
    }

    public void SetPosition(Vector3 position)
    {
        worldPosition = position;
        transform.Translate(position);
    }
    
}
