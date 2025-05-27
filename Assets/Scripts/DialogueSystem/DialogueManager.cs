using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    Dictionary<string, int> traits = new Dictionary<string, int> {
        { "equality", 0 },
        { "authority", 0 },
        { "cynicism", 0 },
        { "individual", 0 },
        { "collective", 0 }
    };

    public DialogueBlock currentBlock;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Debug.Log("Here comes the Start function!!!");
        currentBlock = LoadDialogueBlockFromFile("2015_complete.json");

        var uiManager = FindObjectOfType<DialogueUIManager>();
        uiManager.dialoguePanel.SetActive(true);
        uiManager.choiceContainer.gameObject.SetActive(false);

        uiManager.StartDialogue(currentBlock.dialogues, () =>
        {
            uiManager.DisplayChoices(currentBlock.choices, traits);
        });

        Debug.Log($"[씬 ID] {currentBlock.sceneId}");
        foreach (var d in currentBlock.dialogues)
            Debug.Log($"[대사] {d.speaker}: {d.text_kr}");

        foreach (var c in currentBlock.choices)
            Debug.Log($"[선택지] {c.id}: {c.text_kr} → 효과 {c.effects.Count}개");
    }


    public void ApplyEffects(List<ChoiceEffect> effects)
    {
        foreach (var effect in effects)
        {
            if (!traits.ContainsKey(effect.trait)) continue;

            switch (effect.mode)
            {
                case "add":
                    traits[effect.trait] += effect.value;
                    break;
                case "set":
                    traits[effect.trait] = effect.value;
                    break;
                    // 향후 필요 시 다른 mode 확장 가능
            }

            Debug.Log($"[효과 적용] {effect.trait} → {traits[effect.trait]}");
        }
    }

    public bool EvaluateCondition(string condition, Dictionary<string, int> traits, bool defaultValue)
    {
        if (string.IsNullOrEmpty(condition))
            return defaultValue;  // 이 defaultValue는 available/locked 판단할 때 다르게 넣어줘야 함

        var tokens = condition.Split(' ');
        if (tokens.Length != 3) return false;

        string trait = tokens[0];
        string op = tokens[1];
        if (!int.TryParse(tokens[2], out int value)) return false;

        traits.TryGetValue(trait, out int currentValue);

        return op switch
        {
            ">=" => currentValue >= value,
            "<=" => currentValue <= value,
            ">" => currentValue > value,
            "<" => currentValue < value,
            "==" => currentValue == value,
            "!=" => currentValue != value,
            _ => false
        };
    }

    public DialogueBlock LoadDialogueBlockFromFile(string filename)
    {
        string path = Path.Combine(Application.streamingAssetsPath, filename);
        string json = File.ReadAllText(path);
        DialogueBlock block = JsonUtility.FromJson<DialogueBlock>(json);
        return block;
    }

    public void ShowChoices()
    {
        var uiManager = FindObjectOfType<DialogueUIManager>();
        uiManager.DisplayChoices(currentBlock.choices, traits);
    }
}