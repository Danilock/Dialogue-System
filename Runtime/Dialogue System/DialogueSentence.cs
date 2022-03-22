using System;
using UnityEngine;

namespace DialogueSystem
{
    [Serializable]
    public class DialogueSentence
    {
        public string NodeGUID;
        public string DialogueText;
        public DialogueCharacter Character;
        public AudioClip AudioClip;
        public Vector2 Position;
    }
}