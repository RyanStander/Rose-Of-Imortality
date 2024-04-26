using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TriggerTimeline : MonoBehaviour
{
    [SerializeField] private PlayableDirector playableDirector;
    [SerializeField] private TimelineAsset timeline;
    private bool hasPlayed;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&&!hasPlayed)
        {
            playableDirector.playableAsset = timeline;
            playableDirector.Play();
            hasPlayed = true;
        }
    }
}
