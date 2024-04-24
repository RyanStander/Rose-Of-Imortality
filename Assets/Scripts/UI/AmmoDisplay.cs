using TMPro;
using UnityEngine;

namespace UI
{
    public class AmmoDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI ammoText;
        [SerializeField] private TextMeshProUGUI reloadCost;
        private int maxAmmo;

        public void SetValues(int maxAmmo, int currentAmmo, int reloadPrice)
        {
            this.maxAmmo = maxAmmo;
              ammoText.text = $"{currentAmmo}/{maxAmmo}";
              
              //Determine whether reload cost is seconds, minutes, hours, days, weeks, months, years
                if (reloadPrice < 60)
                {
                    reloadCost.text = $"{reloadPrice} seconds";
                }
                else if (reloadPrice < 3600)
                {
                    reloadCost.text = $"{reloadPrice / 60} minutes";
                }
                else if (reloadPrice < 86400)
                {
                    reloadCost.text = $"{reloadPrice / 3600} hours";
                }
                else if (reloadPrice < 604800)
                {
                    reloadCost.text = $"{reloadPrice / 86400} days";
                }
                else if (reloadPrice < 2628000)
                {
                    reloadCost.text = $"{reloadPrice / 604800} weeks";
                }
                else if (reloadPrice < 31536000)
                {
                    reloadCost.text = $"{reloadPrice / 2628000} months";
                }
                else
                {
                    reloadCost.text = $"{reloadPrice / 31536000} years";
                }
        }
        
        public void SetAmmo(int currentAmmo)
        {
            ammoText.text = $"{currentAmmo}/{maxAmmo}";
        }
    }
}
