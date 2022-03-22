using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using EventDriven;

namespace DialogueSystem
{
    public class DialogueManager : PersistentSingleton<DialogueManager>
    {
        private List<DialogueSentence> _sentences = new List<DialogueSentence>();

        private DialogueSentence _currentSentence;
        private DialogueOption _currentNode;
        private DialogueConversation _currentConversation;

        private bool _isOngoingConversation = false;

        [Header("Player Distance From Initiator")] 
        [SerializeField] private float _maxDistance;

        [SerializeField] private GameObject _player, _initiator;

        [SerializeField] private bool _canCheckDistance = false;

        protected override void Awake()
        {
            base.Awake();
            
            _player = GameObject.FindWithTag("Player");
        }

        private void Update()
        {
            CheckDistance();
        }

        private void CheckDistance()
        {
            if (!_canCheckDistance)
                return;
            var distance = Vector3.Distance(_player.transform.position, _initiator.transform.position);
            
            Debug.Log(distance);
            
            if(distance > _maxDistance)
                EndConversation();
        }

        /// <summary>
        /// Says the next sentence of the current conversation.
        /// </summary>
        public void SayNextSentence()
        {
            if (IsEndOfConversation())
                return;
            
            _currentSentence = GetSentenceBaseOnGUID(_currentNode.TargetNodeGUID, _currentConversation);
            
            EventManager.TriggerEvent(new DialogueSentenceSaid(_currentSentence, GetOptionsOfSentence(_currentSentence, _currentConversation)));

            _currentNode = GetNodeBaseOnSentenceGUID(_currentSentence, _currentConversation);
        }

        public void SaySentence(string targetNode)
        {
            _currentSentence = GetSentenceBaseOnGUID(targetNode, _currentConversation);
            
            EventManager.TriggerEvent(new DialogueSentenceSaid(_currentSentence, GetOptionsOfSentence(_currentSentence, _currentConversation)));

            _currentNode = GetNodeBaseOnSentenceGUID(_currentSentence, _currentConversation);
        }
        
        /// <summary>
        /// Starts the given conversation.
        /// </summary>
        /// <param name="conversationToStart"></param>
        /// <param name="initiator">Who initiated this conversation.</param>
        public void StartConversation(DialogueConversation conversationToStart, GameObject initiator)
        {
            if(_isOngoingConversation)
                return;
            
            _currentConversation = conversationToStart;

            _currentNode = conversationToStart.NodeLinks[0];

            _isOngoingConversation = true;

            _initiator = initiator;

            _canCheckDistance = true;

            SayNextSentence();
            
            EventManager.TriggerEvent(new DialogueStart(conversationToStart, initiator));
        }

        public void EndConversation()
        {
            _isOngoingConversation = false;

            _currentConversation = null;
            _currentNode = null;
            _currentSentence = null;

            _canCheckDistance = false;
            
            EventManager.TriggerEvent(new DialogueEnd(_currentConversation));
        }

        public DialogueSentence GetSentenceBaseOnGUID(string GUID, DialogueConversation conversation)
        {
            return conversation.DialogueNodeData.Find(x => x.NodeGUID == GUID);
        }

        public List<DialogueOption> GetOptionsOfSentence(DialogueSentence sentence, DialogueConversation conversation)
        {
            return conversation.NodeLinks.FindAll(x => x.BaseNodeGUID == sentence.NodeGUID);
        }

        public DialogueOption GetNodeBaseOnSentenceGUID(DialogueSentence sentence, DialogueConversation conversation)
        {
            return conversation.NodeLinks.Find(x => x.BaseNodeGUID == sentence.NodeGUID);
        }

        private bool IsEndOfConversation()
        {
            if (_currentSentence != null)
            {
                if (GetNodeBaseOnSentenceGUID(_currentSentence, _currentConversation) == null)
                {
                    EndConversation();
                    return true;
                }
            }

            return false;
        }
    }
}