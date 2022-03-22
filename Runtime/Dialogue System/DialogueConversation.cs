using System;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    [Serializable]
    public class DialogueConversation : ScriptableObject
    {
        public List<DialogueOption> NodeLinks = new List<DialogueOption>();
        public List<DialogueSentence> DialogueNodeData = new List<DialogueSentence>();
        public List<ExposedProperty> ExposedProperties = new List<ExposedProperty>();
        public List<CommentBlockData> CommentBlockData = new List<CommentBlockData>();
    }
}