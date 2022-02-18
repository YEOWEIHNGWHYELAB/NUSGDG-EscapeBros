using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth * 1f;
        slider.value = maxHealth * 1f;
    }

    public void SetHealth(int health)
    {
        slider.value = health * 1f;
    }
}
