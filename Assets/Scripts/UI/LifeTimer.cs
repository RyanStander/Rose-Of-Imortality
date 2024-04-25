using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class LifeTimer : MonoBehaviour
    {
        //TODO: we'd like to show it as years-weeks-days-hours-minutes-seconds
        
        [SerializeField] private TextMeshProUGUI lifeTimeText;
        [SerializeField] private float lifetimeSpeed = 2f;
        private float currentLifetime;
        private float targetLifetime;
        private void OnValidate()
        {
            if (lifeTimeText == null)
                lifeTimeText = GetComponent<TextMeshProUGUI>();
        }
        
        public void Setup(float lifetime)
        {
            currentLifetime = lifetime;
            targetLifetime = lifetime;
        }

        private void FixedUpdate()
        {
            if (lifeTimeText != null)
            {
                currentLifetime = Mathf.Lerp(currentLifetime, targetLifetime, lifetimeSpeed * Time.deltaTime);
                
                int years = Mathf.FloorToInt(currentLifetime / 31536000f);
                int weeks = Mathf.FloorToInt(currentLifetime / 604800f) % 52;
                int days = Mathf.FloorToInt(currentLifetime / 86400f) % 7;
                int hours = Mathf.FloorToInt(currentLifetime / 3600f) % 24;
                int minutes = Mathf.FloorToInt((currentLifetime % 3600f) / 60f);
                int seconds = Mathf.FloorToInt(currentLifetime % 60f);

                lifeTimeText.text =  years.ToString("000") + ":" + weeks.ToString("00") + ":" + days.ToString("0") + ":" + hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
            }
        }

        public void UpdateLifetimeUI(float newLifetime)
        {
            targetLifetime = newLifetime;
        }
    }
}
