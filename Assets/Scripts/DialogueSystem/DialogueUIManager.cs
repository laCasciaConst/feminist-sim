using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject choiceButtonPrefabA;
    public GameObject choiceButtonPrefabB;
    public GameObject choiceButtonPrefabC;
    public GameObject choiceButtonPrefabD;
    public Transform choiceContainer;
    public TextMeshProUGUI labelText;
    public TextMeshProUGUI bodyText;
    public GameObject dialoguePanel;

    public enum SupportedLanguage { Korean, French }

    [Header("Language Setting")]
    public SupportedLanguage currentLanguage = SupportedLanguage.Korean;

    [Header("Font Assets")]
    public TMP_FontAsset koreanFontAsset;
    public TMP_FontAsset frenchFontAsset;

    private Coroutine followUpCoroutine;

    public void DisplayChoices(List<DialogueChoice> choices, Dictionary<string, int> traits)
    {

        foreach (Transform child in choiceContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (var choice in choices)
        {
            var buttonObj = Instantiate(GetPrefabByChoiceId(choice.id), choiceContainer);
            var button = buttonObj.GetComponent<Button>();
            var text = buttonObj.GetComponentInChildren<TMP_Text>();

            Debug.Log($"[버튼 생성] {choice.id}");
            Debug.Log($"[텍스트 찾음?] {(text != null ? "예" : "아니오")}");

            // 언어별 텍스트 설정
            string localizedText = currentLanguage == SupportedLanguage.Korean ? choice.text_kr : choice.text_fr;
            text.text = localizedText;

            // 언어별 폰트 설정
            text.font = currentLanguage == SupportedLanguage.Korean ? koreanFontAsset : frenchFontAsset;

            // 조건 판별
            bool isAvailable = DialogueManager.Instance.EvaluateCondition(choice.availableIf, traits, true);
            bool isLocked = DialogueManager.Instance.EvaluateCondition(choice.lockedIf, traits, false);
            button.interactable = isAvailable && !isLocked;
            
            Debug.Log($"[버튼 활성화 평가] {choice.id} → isAvailable: {isAvailable}, isLocked: {isLocked}");
            if (!isAvailable || isLocked)
                Debug.LogWarning($"[비활성 선택지] {choice.id}가 비활성화됨");

            // 클릭 시 이벤트
            button.onClick.AddListener(() =>
            {
                Debug.Log($"[선택] {choice.id}");
                DialogueManager.Instance.ApplyEffects(choice.effects);

                List<string> followUp = currentLanguage == SupportedLanguage.Korean ? choice.followUp_kr : choice.followUp_fr;
                DisplayFollowUp(followUp);
            });
        }
    }

    private GameObject GetPrefabByChoiceId(string id)
    {
        if (string.IsNullOrEmpty(id)) return choiceButtonPrefabA;

        char last = id[id.Length - 1];
        GameObject prefab = last switch
        {
            'A' => choiceButtonPrefabA,
            'B' => choiceButtonPrefabB,
            'C' => choiceButtonPrefabC,
            'D' => choiceButtonPrefabD,
            _ => choiceButtonPrefabA
        };

        if (prefab == null)
        {
            Debug.LogError($"[프리팹 오류] '{id}'에 해당하는 프리팹이 null입니다!");
        }
        else
        {
            Debug.Log($"[프리팹 정상] '{id}' → {prefab.name}");
        }

        return prefab;
    }


    public void DisplayFollowUp(List<string> lines)
    {
        if (followUpCoroutine != null)
            StopCoroutine(followUpCoroutine);

        followUpCoroutine = StartCoroutine(ShowLinesCoroutine(lines));
    }

    private IEnumerator ShowLinesCoroutine(List<string> lines)
    {
        dialoguePanel.SetActive(true);

        foreach (var line in lines)
        {
            bodyText.text = line;
            labelText.text = ""; // 필요시 화자 이름 추출
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }

        // 대사 끝난 뒤 선택지 보여주기
        DialogueManager.Instance.ShowChoices();

        Debug.Log("[후속 대사 종료]");
    }

    public void StartDialogue(List<DialogueLine> lines)
    {
        List<string> localizedLines = new List<string>();
        foreach (var line in lines)
        {
            string text = currentLanguage == SupportedLanguage.Korean ? line.text_kr : line.text_fr;
            localizedLines.Add(text);
        }

        TMP_FontAsset selectedFont = currentLanguage == SupportedLanguage.Korean ? koreanFontAsset : frenchFontAsset;
        bodyText.font = selectedFont;
        labelText.font = selectedFont;

        StartCoroutine(ShowLinesCoroutine(localizedLines));

    }
}
