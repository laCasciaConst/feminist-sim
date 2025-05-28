using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

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
    private bool isTyping = false;

    public void DisplayChoices(List<DialogueChoice> choices, Dictionary<string, int> traits)
    {
        choiceContainer.gameObject.SetActive(true);

        foreach (Transform child in choiceContainer)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < choices.Count; i++)
        {
            var choice = choices[i];
            var buttonObj = Instantiate(GetPrefabByChoiceId(choice.id), choiceContainer);
            var button = buttonObj.GetComponent<Button>();
            var text = buttonObj.GetComponentInChildren<TMP_Text>();

            float delay = i * 0.1f;

            // Debug.Log($"[버튼 생성] {choice.id}");
            // Debug.Log($"[텍스트 찾음?] {(text != null ? "예" : "아니오")}");

            Vector3 originalPos = buttonObj.transform.localPosition;
            buttonObj.transform.DOLocalMoveY(originalPos.y + 10f, 0.5f)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.Linear)
                .SetId(buttonObj);

            string localizedText = currentLanguage == SupportedLanguage.Korean ? choice.text_kr : choice.text_fr;
            text.text = localizedText;
            text.font = currentLanguage == SupportedLanguage.Korean ? koreanFontAsset : frenchFontAsset;

            bool isAvailable = DialogueManager.Instance.EvaluateCondition(choice.availableIf, traits, true);
            bool isLocked = DialogueManager.Instance.EvaluateCondition(choice.lockedIf, traits, false);
            button.interactable = isAvailable && !isLocked;

            // Debug.Log($"[버튼 활성화 평가] {choice.id} → isAvailable: {isAvailable}, isLocked: {isLocked}");
            // if (!isAvailable || isLocked)
            //     Debug.LogWarning($"[비활성 선택지] {choice.id}가 비활성화됨");

            buttonObj.transform.localScale = Vector3.zero;
            CanvasGroup cg = buttonObj.AddComponent<CanvasGroup>();
            cg.alpha = 0f;

            buttonObj.transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack).SetDelay(delay);
            cg.DOFade(1f, 0.3f).SetDelay(delay);

            button.onClick.AddListener(() =>
            {
                Debug.Log($"[선택] {choice.id}");
                DialogueManager.Instance.ApplyEffects(choice.effects);

                foreach (Transform sibling in choiceContainer)
                {
                    if (sibling != buttonObj.transform)
                    {
                        DOTween.Kill(sibling);
                        sibling.gameObject.SetActive(false);
                    }
                }

                DOTween.Kill(buttonObj);
                buttonObj.transform.DOScale(1.2f, 0.25f)
                    .SetEase(Ease.OutBack)
                    .OnComplete(() =>
                    {
                        StartCoroutine(HideAndFollowUp(buttonObj, choice));
                    });
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

    private IEnumerator HideAndFollowUp(GameObject selectedButton, DialogueChoice choice)
    {
        yield return new WaitForSeconds(0.7f);
        selectedButton.SetActive(false);
        List<string> followUp = currentLanguage == SupportedLanguage.Korean ? choice.followUp_kr : choice.followUp_fr;
        DisplayFollowUp(followUp, () => StartCoroutine(FadeTransition(() => DialogueManager.Instance.GoToNextScene())));
    }

    public void DisplayFollowUp(List<string> lines, System.Action onComplete = null)
    {
        if (followUpCoroutine != null)
            StopCoroutine(followUpCoroutine);

        followUpCoroutine = StartCoroutine(ShowLinesCoroutine(lines, onComplete));
    }

    public void StartDialogue(List<DialogueLine> lines, System.Action onDialogueComplete = null)
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

        StartCoroutine(ShowLinesCoroutine(localizedLines, onDialogueComplete));
    }

    IEnumerator TypeLine(string line)
    {
        isTyping = true;
        bodyText.text = "";

    foreach (char c in line)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bodyText.text = line;
            break; // 남은 글자 생략
        }

        bodyText.text += c;
        yield return new WaitForSeconds(0.05f);
    }

    isTyping = false;
    }

    private IEnumerator ShowLinesCoroutine(List<string> lines, System.Action onComplete = null)
    {
        dialoguePanel.SetActive(true);

        foreach (var line in lines)
        {
            labelText.text = "";
            Coroutine typingCoroutine = StartCoroutine(TypeLine(line));

            while (isTyping)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    StopCoroutine(typingCoroutine);
                    bodyText.text = line;
                    isTyping = false;
                    break;
                }
                yield return null;
            }
            yield return new WaitForSeconds(0.2f);

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.Space));
        }

        Debug.Log("[대사 끝남] 콜백 실행 직전");
        onComplete?.Invoke();
        Debug.Log("[후속 대사 종료]");
    }

    public Image fadePanel;

    // 페이드 함수
    public IEnumerator FadeTransition(System.Action onMidpoint = null)
    {
        fadePanel.gameObject.SetActive(true);
        fadePanel.color = new Color(0, 0, 0, 0);
        yield return fadePanel.DOFade(2f, 1f).WaitForCompletion();
        Debug.Log("[페이드 완료] 다음 씬 호출 시도");
        onMidpoint?.Invoke();  // 중간 지점에서 씬 전환

        yield return new WaitForSeconds(0.2f);
        yield return fadePanel.DOFade(0f, 0.5f).WaitForCompletion();
        fadePanel.gameObject.SetActive(false);
    }
}
