using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{

    [SerializeField] private Slider slider;
    // [SerializeField] private Camera cammera;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Image fillImage;
    [SerializeField] private RectTransform fillArea;
    [SerializeField] private GameObject enemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void UpdateHealthBar(float currentValue, float maxValue)
    {

        slider.value = currentValue / maxValue;
        if (slider.value > 0.5f)
        {
            fillImage.color = Color.green;
        }
        else if (slider.value <= 0.5f && slider.value > 0.15f)
        {
            fillImage.color = Color.yellow;
        }
        else
        {
            fillImage.color = Color.red;
        }
        // if (currentValue >= (maxValue / 50))
        // {
        //     fillImage.color = Color.green;
        // }
        // else if (currentValue <= (maxValue / 50) && currentValue >= (maxValue / 15))
        // {
        //     fillImage.color = Color.yellow;
        // }
        // else
        // {
        //     fillImage.color = Color.red;
        // }


    }

    // Update is called once per frame
    void Update()
    {
        // transform.rotation = cammera.transform.rotation;
        transform.position = target.position + offset;
        transform.localScale = Vector3.one;
        fillArea.localScale = enemy.transform.localScale;
    }
}
