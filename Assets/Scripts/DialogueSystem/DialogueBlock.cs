using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueChoice
{
    public string id;
    public string text_kr;
    public string text_fr;
    public List<ChoiceEffect> effects;
    public List<string> followUp_kr;
    public List<string> followUp_fr;
    public string availableIf;
    public string lockedIf;
}

// [System.Serializable]
    // public class ChoiceGroup {
    //     public string ChoiceGroupID;
    //     public List<DialogueChoice> Choices;
    // }

    [System.Serializable]
public class DialogueLine
{
    public string speaker;
    public string label_kr;
    public string text_kr;
    public string label_fr;
    public string text_fr;
}

[System.Serializable]
public class DialogueBlock
{
    public string sceneId;
    public List<DialogueLine> dialogues;
    public List<DialogueChoice> choices;
}


[System.Serializable]
public class ChoiceEffect
{
    public string trait;
    public int value;
    public string mode;
}

[System.Serializable]
public class DialogueBlockListWrapper {
    public List<DialogueBlock> blocks;
}