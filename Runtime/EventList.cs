using DialogueSystem;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Struct used to hold data once a dialogue starts.
/// </summary>
public struct DialogueStart
{
    public DialogueConversation Conversation;
    public GameObject Initiator;
    
    public DialogueStart(DialogueConversation conversation, GameObject initiator)
    {
        Conversation = conversation;
        Initiator = initiator;
    }
}

/// <summary>
/// Struct used to hold data once a dialogue sentence is said.
/// </summary>
public struct DialogueSentenceSaid
{
    public DialogueSentence Sentence;
    public List<DialogueOption> Options;
    public bool HasOptions => Options.Count > 0;

    public DialogueSentenceSaid(DialogueSentence sentence, List<DialogueOption> options)
    {
        Sentence = sentence;
        Options = options;
    }
}

/// <summary>
/// Struct used to hold data once a dialogue ends.
/// </summary>
public struct DialogueEnd
{
    public DialogueConversation Conversation;

    public DialogueEnd(DialogueConversation conversation) => Conversation = conversation;
}

public enum GameEventsTypes
{
    LevelEnd,
    LevelStart
}

public struct GameEvent
{
    public GameEventsTypes EventTypes;

    public GameEvent(GameEventsTypes eventsTypes)
    {
        EventTypes = eventsTypes;
    }
}