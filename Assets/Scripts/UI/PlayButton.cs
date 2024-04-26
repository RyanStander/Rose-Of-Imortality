using UnityEngine;

namespace UI
{
    public class PlayButton : MonoBehaviour
    {
        public void PlayGame()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        }
    }
}
