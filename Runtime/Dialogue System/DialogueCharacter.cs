using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    [CreateAssetMenu(fileName = "Character", menuName = "Dialogue/Character")]
    public class DialogueCharacter : ScriptableObject
    {
        public string Name;
    }
}