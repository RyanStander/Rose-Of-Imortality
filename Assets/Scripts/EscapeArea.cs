using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// When the player enters the collider, the game will fade to black and then return to the main menu
/// </summary>
public class EscapeArea : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeSpeed = 1f;
    [SerializeField] private string mainMenuSceneName;
    
    private bool isFading;
    
    private IEnumerator SlowFadeToBlackAndLoadScene()
    {
        var color = fadeImage.color;
        while (color.a < 1)
        {
            color.a += fadeSpeed * Time.deltaTime;
            fadeImage.color = color;
            yield return null;
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(mainMenuSceneName);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&&!isFading)
        {
            isFading = true;
            StartCoroutine(SlowFadeToBlackAndLoadScene());
        }
    }
}
