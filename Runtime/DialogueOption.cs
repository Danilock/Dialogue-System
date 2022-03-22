using System;
using System.Linq;

namespace DialogueSystem
{
    [Serializable]
    public class DialogueOption
    {
        public string BaseNodeGUID;
        public string PortName;
        public string TargetNodeGUID;
    }
}