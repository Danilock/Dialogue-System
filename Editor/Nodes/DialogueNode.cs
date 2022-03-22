using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace DialogueSystem
{
    public class DialogueNode : Node
    {
        public string DialogueText;
        public DialogueCharacter DialogueCharacter;
        public string GUID;
        public AudioClip AudioClip;
        public bool EntyPoint = false;
    }
}