using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    public List<DialogueBlock> currentSceneList;
    private int currentSceneIndex = 0;

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
        currentSceneList = LoadDialogueBlocksFromFile("2015_complete.json");
        currentSceneIndex = 0;

        if (currentSceneList == null || currentSceneList.Count == 0)
        {
            Debug.LogError("[에러] currentSceneList가 비었거나 로딩 실패!");
            return;
        }

        currentSceneIndex = 0;
        PlayScene(currentSceneList[currentSceneIndex]);

        Debug.Log($"[씬 ID] {currentBlock.sceneId}");
        foreach (var d in currentBlock.dialogues)
            Debug.Log($"[대사] {d.speaker}: {d.text_kr}");

        foreach (var c in currentBlock.choices)
            Debug.Log($"[선택지] {c.id}: {c.text_kr} → 효과 {c.effects.Count}개");
    }

    void PlayScene(DialogueBlock block)
    {
        currentBlock = block;
        var uiManager = FindObjectOfType<DialogueUIManager>();

        uiManager.dialoguePanel.SetActive(true);
        uiManager.choiceContainer.gameObject.SetActive(false);

        uiManager.StartDialogue(block.dialogues, () =>
        {
            uiManager.DisplayChoices(block.choices, traits);
        });
    }

    public void GoToNextScene()
    {
        currentSceneIndex++;
        if (currentSceneIndex < currentSceneList.Count)
        {
            PlayScene(currentSceneList[currentSceneIndex]);
        }
        else
        {
            Debug.Log("[엔딩] 더 이상 다음 씬이 없습니다.");
            // 필요 시 엔딩 화면으로 전환 등 처리
        }
    }

    public void ShowChoices()
    {
        var uiManager = FindObjectOfType<DialogueUIManager>();
        uiManager.DisplayChoices(currentBlock.choices, traits);
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

    public List<DialogueBlock> LoadDialogueBlocksFromFile(string filename)
    {
        string path = Path.Combine(Application.streamingAssetsPath, filename);
        string json = File.ReadAllText(path);

        Debug.Log("[원본 JSON 내용]\n" + json);

        json = "{\"blocks\":" + json + "}";

        Debug.Log("[래핑된 JSON 내용]\n" + json);

        DialogueBlockListWrapper wrapper = JsonUtility.FromJson<DialogueBlockListWrapper>(json);

        if (wrapper == null)
        {
            Debug.LogError("[에러] JsonUtility 파싱 실패: wrapper == null");
            return new List<DialogueBlock>();
        }

        if (wrapper.blocks == null)
        {
            Debug.LogError("[에러] 파싱 성공했으나 blocks == null");
            return new List<DialogueBlock>();
        }

        Debug.Log($"[로딩된 씬 수] {wrapper.blocks.Count}");
        return wrapper.blocks;
    }


}