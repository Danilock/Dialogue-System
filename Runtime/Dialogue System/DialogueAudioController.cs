using System;
using System.Collections;
using System.Collections.Generic;
using EventDriven;
using UnityEngine;

namespace DialogueSystem
{
    [RequireComponent(typeof(AudioSource))]
    public class DialogueAudioController : MonoBehaviour, IEventListener<DialogueSentenceSaid>
    {
        [SerializeField] private AudioSource _source;

        private void Start()
        {
            if (_source == null)
                _source = gameObject.GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            this.StartListening<DialogueSentenceSaid>();
        }

        private void OnDisable()
        {
            this.StopListening<DialogueSentenceSaid>();
        }

        public void OnTriggerEvent(DialogueSentenceSaid data)
        {
            _source.Stop();
            
            if(data.Sentence.AudioClip == null)
                return;
            
            _source.clip = data.Sentence.AudioClip;
            
            _source.Play();
        }
    }
}