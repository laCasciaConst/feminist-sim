using System.Collections;
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

    public void Start()
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

        // Debug.Log($"[씬 ID] {currentBlock.sceneId}");
        // foreach (var d in currentBlock.dialogues)
        //     Debug.Log($"[대사] {d.speaker}: {d.text_kr}");

        foreach (var c in currentBlock.choices)
            Debug.Log($"[선택지] {c.id}: {c.text_kr} → 효과 {c.effects.Count}개");

        PlayerPrefs.DeleteAll();   // 또는 DeleteKey("PlayerGender");

    }

    public void PlayScene(DialogueBlock block)
    {
        currentBlock = block;
        var uiManager = DialogueUIManager.Instance;

        uiManager.bodyText.text = "";
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
            Debug.Log("[DEBUG] 다음 씬으로 넘어감");
            StartCoroutine(FullSceneTransition(currentSceneList[currentSceneIndex]));
        }
        else
        {
            Debug.Log("[엔딩] 전체 씬 종료");
        }
    }

    private IEnumerator FullSceneTransition(DialogueBlock nextBlock)
    {
        var ui = DialogueUIManager.Instance;

        // 1. 페이드아웃
        yield return ui.FadeOutAfterDialogue(); // 여기 내부에서 dialoguePanel도 꺼졌다고 가정

        // 2. 약간의 여백 (선택사항)
        yield return new WaitForSeconds(0.2f);

        // 3. 페이드인
        yield return ui.FadeInBeforeDialogue(() => { }); //절대 람다를 넘기는걸 잊지 마씨오..좃되기시르면

        // 4. 약간 대기 후 대사 시작
        // yield return new WaitForSeconds(0.5f);

        PlayScene(nextBlock);
    }

    public void ApplyEffects(List<ChoiceEffect> effects)
    {
        TraitCalculator.ApplyEffects(traits, effects);

        string sid = currentBlock.sceneId;
        if (sid.StartsWith("2016-B-Q") || sid.StartsWith("2017-A-Q"))
        {
            TraitCalculator.ApplyMultipliers(traits, sid);  // 새로운 함수
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

    public static string GetLocalizedText(DialogueLine line)
    {
        var gender = PlayerGender;

        if (gender == Gender.Female && !string.IsNullOrEmpty(line.text_kr_female))
        {
            return line.text_kr_female;
        }
        else if (gender == Gender.Male && !string.IsNullOrEmpty(line.text_kr_male))
        {
            return line.text_kr_male;
        }
        else
        {
            return line.text_kr;
        }
    }


    public List<DialogueBlock> LoadDialogueBlocksFromFile(string filename)
    {
        string path = Path.Combine(Application.streamingAssetsPath, filename);
        string json = File.ReadAllText(path);
        // Debug.Log("[원본 JSON 내용]: " + json);

        json = "{\"blocks\":" + json + "}";
        // Debug.Log("[래핑된 JSON 내용]: " + json);

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

    public enum Gender { Male, Female }

    public static Gender PlayerGender { get; set; } = Gender.Male;

    // public static Gender PlayerGender
    // {
    //     get
    //     {
    //         return (Gender)PlayerPrefs.GetInt("PlayerGender", 0); // 기본값 Male
    //     }
    //     set
    //     {
    //         PlayerPrefs.SetInt("PlayerGender", (int)value);
    //     }
    // }
}