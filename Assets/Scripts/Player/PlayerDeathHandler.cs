using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

namespace Player
{
    public class PlayerDeathHandler : MonoBehaviour
    {
        [SerializeField] private PlayerManager playerManager;
        [SerializeField] private Image fadeOutPanel;
        [SerializeField] private float fadeOutTime = 1f;
        [SerializeField] private string mainMenuSceneName = "MainMenu";
        [SerializeField] private PlayableDirector playableDirector;
        [SerializeField]  private PlayableAsset deathTimeline;
        private bool hasPlayed;
        
        private void OnValidate()
        {
            if (playerManager == null)
                playerManager = GetComponent<PlayerManager>();
            
            if (playableDirector == null)
                playableDirector = GetComponent<PlayableDirector>();
        }
        
        public void HandleDeath()
        {
            playerManager.Inputs.enabled = false;
            StartCoroutine( SlowFadeToBlackAndLoadScene());
        }
        
        private IEnumerator SlowFadeToBlackAndLoadScene()
        {
            if (!hasPlayed)
            {
                playableDirector.playableAsset = deathTimeline;
                playableDirector.Play();
                hasPlayed = true;
            }
            
            var color = fadeOutPanel.color;
            while (color.a < 1)
            {
                color.a += fadeOutTime * Time.deltaTime;
                fadeOutPanel.color = color;
                yield return null;
            }
            UnityEngine.SceneManagement.SceneManager.LoadScene(mainMenuSceneName);
        }
    }
}
