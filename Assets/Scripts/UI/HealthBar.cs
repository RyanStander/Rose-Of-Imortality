using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        private float currentValue;
        private float targetValue;
        [SerializeField]private float changeSpeed = 0.5f;

        private void OnValidate()
        {
            if (slider == null)
                slider = GetComponent<Slider>();
        }

        private void FixedUpdate()
        {
            if (Math.Abs(currentValue - targetValue) > 0.01f)
            {
                currentValue = Mathf.Lerp(currentValue, targetValue, changeSpeed * Time.deltaTime);
                slider.value = currentValue;
            }
        }

        public void SetMaxHealth(int maxHealth)
        {
            slider.maxValue = maxHealth;
            slider.value = maxHealth;
            targetValue = maxHealth;
            currentValue = maxHealth;
        }
        
        public void SetHealth(int health)
        {
            targetValue = health;
        }
         
    }
}
