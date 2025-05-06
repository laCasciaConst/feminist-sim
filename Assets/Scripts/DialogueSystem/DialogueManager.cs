using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    Dictionary<string, int> traits = new Dictionary<string, int> {
    { "egalitarianism", 0 },
    { "authoritarianism", 0 },
    { "cynicism", 0 },
    { "individualism", 0 },
    { "solidarity", 0 }
};

    public DialogueBlock currentBlock;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // 중복 방지
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // 씬 변경에도 유지
    }

    void Start()
    {
        currentBlock = LoadMockDialogueBlock(); // 테스트용
        var available = GetAvailableChoices(currentBlock.Choices, traits);
        foreach (var c in available)
        {
            Debug.Log($"[활성화됨] {c.text_ko} / {c.text_fr}");
        }
    }

    public List<Choice> GetAvailableChoices(List<Choice> allChoices, Dictionary<string, int> traits)
    {
        List<Choice> filtered = new List<Choice>();

        foreach (var choice in allChoices)
        {
            bool available = EvaluateCondition(choice.availableIf, traits);
            bool locked = EvaluateCondition(choice.lockedIf, traits);

            if (available && !locked)
            {
                filtered.Add(choice);
            }
            else
            {
                Debug.Log($"[잠김] {choice.text_ko} / {choice.text_fr}");
            }
        }

        return filtered;
    }

    public bool EvaluateCondition(string condition, Dictionary<string, int> traits)
    {
        if (string.IsNullOrEmpty(condition)) return true;

        string[] parts = condition.Split(' ');
        if (parts.Length != 3) return false;

        string trait = parts[0];
        string op = parts[1];
        if (!int.TryParse(parts[2], out int value)) return false;

        if (!traits.TryGetValue(trait, out int current)) return false;

        return op switch
        {
            ">=" => current >= value,
            "<=" => current <= value,
            ">" => current > value,
            "<" => current < value,
            "==" => current == value,
            "!=" => current != value,
            _ => false
        };
    }

    // 테스트용 데이터
    DialogueBlock LoadMockDialogueBlock()
    {
        return new DialogueBlock
        {
            SceneID = "scene001",
            Choices = new List<Choice> {
                new Choice {
                    id = "c1",
                    text_ko = "하고 싶으면 해",
                    text_fr = "Fais-le si tu veux",
                    effect = "individualism += 1",
                    availableIf = "",
                    lockedIf = "authoritarianism >= 3"
                },
                new Choice {
                    id = "c2",
                    text_ko = "그건 좀 이상하지 않아?",
                    text_fr = "C’est un peu bizarre, non ?",
                    effect = "authoritarianism += 1",
                    availableIf = "cynicism < 2",
                    lockedIf = ""
                },
                new Choice {
                    id = "c3",
                    text_ko = "잘 모르겠어",
                    text_fr = "Je ne sais pas trop",
                    effect = "",
                    availableIf = "",
                    lockedIf = ""
                }
            }
        };
    }
}
