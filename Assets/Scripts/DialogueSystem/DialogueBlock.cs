using System.Collections.Generic;

[System.Serializable]
public class Choice {
    public string id;
    public string effect;
    public string availableIf;
    public string lockedIf;
}

[System.Serializable]
public class ChoiceGroup {
    public string ChoiceID;
    public List<Choice> Choices;
}

[System.Serializable]
public class DialogueLine {
    public string id;
    public string Speaker;
    public string Emotion;
}

[System.Serializable]
public class DialogueBlock {
    public string SceneID;
    public List<DialogueLine> DialogueBlock;
    public ChoiceGroup ChoiceGroup;
}
