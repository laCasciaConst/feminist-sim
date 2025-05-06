using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUIManager : MonoBehaviour
{
    public GameObject choiceButtonPrefab; // Button 프리팹
    public Transform choiceContainer;     // 선택지 부모 오브젝트
    public SystemLanguage currentLanguage = SystemLanguage.Korean; // 혹은 자동 감지

    public void DisplayChoices(List<Choice> choices, Dictionary<string, int> traits)
    {
        // 기존 버튼 제거
        foreach (Transform child in choiceContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (var choice in choices)
        {
            var buttonObj = Instantiate(choiceButtonPrefab, choiceContainer);
            var button = buttonObj.GetComponent<Button>();
            var text = buttonObj.GetComponentInChildren<TMP_Text>();

            string localizedText = currentLanguage switch
            {
                SystemLanguage.Korean => choice.text_ko,
                SystemLanguage.French => choice.text_fr,
                _ => choice.text_ko
            };
            text.text = localizedText;

            bool isAvailable = DialogueManager.Instance.EvaluateCondition(choice.availableIf, traits);
            bool isLocked = DialogueManager.Instance.EvaluateCondition(choice.lockedIf, traits);

            button.interactable = isAvailable && !isLocked;

            // 클릭 시 효과 처리 연결
            button.onClick.AddListener(() =>
            {
                Debug.Log($"선택: {choice.id} | 효과: {choice.effect}");
                // 효과 파싱 및 적용 필요 시 여기서 추가
            });
        }
    }
}
