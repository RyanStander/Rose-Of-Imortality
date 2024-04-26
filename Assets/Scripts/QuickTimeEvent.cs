using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

/// <summary>
/// When the player enters a trigger, it will start a timeline and have a quick time event where they either press q or e. When either is pressed it will start a different timeline based on the button
/// </summary>
public class QuickTimeEvent : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    
    [SerializeField] private PlayableDirector playableDirector;
    [SerializeField] private TimelineAsset quickTimeEventTimeline;
    [SerializeField] private List<GameObject> objectToEnableOnQteStart;
    [SerializeField] private List<GameObject> objectToDisableOnQteStart;
    [SerializeField] private GameObject quickTimeUi;

    [SerializeField] private TimelineAsset selflessTimeline;
    [SerializeField] private List<GameObject> objectToEnableOnSelflessStart;
    [SerializeField] private List<GameObject> objectToDisableOnSelflessStart;

    [SerializeField] private TimelineAsset selfishTimeline;
    [SerializeField] private List<GameObject> objectToEnableOnSelfishStart;
    [SerializeField] private List<GameObject> objectToDisableOnSelfishStart;

    private bool startedQte;
    private bool QteCompleted;
    private Quaternion playerRotation;

    private void OnValidate()
    {
        if (playerTransform == null)
            playerTransform = FindObjectOfType<Player.PlayerManager>().transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (startedQte)
            return;

        if (other.CompareTag("Player"))
        {
            StartQuickTimeEvent();
            playerRotation = playerTransform.rotation;
        }
    }

    private void Update()
    {
        if (startedQte&&!QteCompleted)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                //give player 998 years of lifespan
                var lifespan = playerTransform.GetComponent<Player.PlayerManager>().PlayerLifetime;
                lifespan.SpendTime(-31536000f * 998f);
                
                playableDirector.playableAsset = selfishTimeline;
                playableDirector.Play();
                quickTimeUi.SetActive(false);
                QteCompleted = true;

                foreach (var obj in objectToEnableOnSelfishStart)
                {
                    obj.SetActive(true);
                }

                foreach (var obj in objectToDisableOnSelfishStart)
                {
                    obj.SetActive(false);
                }
                
                playerTransform.rotation =  playerRotation;
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                playableDirector.playableAsset = selflessTimeline;
                playableDirector.Play();
                quickTimeUi.SetActive(false);
                QteCompleted = true;

                foreach (var obj in objectToEnableOnSelflessStart)
                {
                    obj.SetActive(true);
                }

                foreach (var obj in objectToDisableOnSelflessStart)
                {
                    obj.SetActive(false);
                }
                
                playerTransform.rotation =  playerRotation;
            }
        }
    }

    private void StartQuickTimeEvent()
    {
        playableDirector.playableAsset = quickTimeEventTimeline;
        playableDirector.Play();
        quickTimeUi.SetActive(true);
        startedQte = true;

        foreach (var obj in objectToEnableOnQteStart)
        {
            obj.SetActive(true);
        }

        foreach (var obj in objectToDisableOnQteStart)
        {
            obj.SetActive(false);
        }
    }
}
