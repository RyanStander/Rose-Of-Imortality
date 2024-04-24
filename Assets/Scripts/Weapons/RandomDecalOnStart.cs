using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Weapons
{
    public class RandomDecalOnStart : MonoBehaviour
    {
        [SerializeField] private DecalProjector decalProjector;

        private void OnValidate()
        {
            if (decalProjector == null)
                decalProjector = GetComponent<DecalProjector>();
        }
        
        private void Start()
        {
            //Get the amount of sprites
            var xSize = (int)(1 / decalProjector.uvScale.x);
            var ySize = (int)(1 / decalProjector.uvScale.y);
            
            var randomX = Random.Range(1, xSize+1);
            var randomY = Random.Range(1, ySize+1);
            
            var randomUv = new Vector2((float)randomX/ySize-decalProjector.uvScale.x, (float)randomY/ySize-decalProjector.uvScale.y);
            
            
            decalProjector.uvBias = randomUv;
        }
    }
}
