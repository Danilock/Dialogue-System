using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    /// <summary>
    /// Triggers a conversation once this object is touched by the player.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] private DialogueConversation _conversationToStart;

        [SerializeField] private GameObject _objectToSend;
        
        private void OnTriggerEnter(Collider other)
        {
            if(!other.CompareTag("Player"))
                return;
            
            DialogueManager.Instance.StartConversation(_conversationToStart, _objectToSend);
        }
    }
}