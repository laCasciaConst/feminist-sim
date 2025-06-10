using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerNameInputManager : MonoBehaviour
{
    public GameObject nameInputPanel;
    public TMP_InputField nameInputField;
    public Button confirmButton;
    public Button maleButton;
    public Button femaleButton;

    private DialogueManager.Gender selectedGender = DialogueManager.Gender.Male;

    void Start()
    {
        // 이름 항상 새로 입력하도록 강제 초기화
        PlayerPrefs.DeleteKey("PlayerName");

        nameInputPanel.SetActive(true);
        confirmButton.onClick.AddListener(OnConfirmName);
        maleButton.onClick.AddListener(() => SelectGender(DialogueManager.Gender.Male));
        femaleButton.onClick.AddListener(() => SelectGender(DialogueManager.Gender.Female));
    }

    void SelectGender(DialogueManager.Gender gender)
    {
        selectedGender = gender;

        var normal = Color.white;
        var selected = new Color(0.7f, 0.9f, 1f);

        maleButton.GetComponent<Image>().color = (gender == DialogueManager.Gender.Male) ? selected : normal;
        femaleButton.GetComponent<Image>().color = (gender == DialogueManager.Gender.Female) ? selected : normal;
    }

    void OnConfirmName()
    {
        string inputName = nameInputField.text.Trim();
        if (string.IsNullOrEmpty(inputName)) return;

        YourPlayerNameManager.CurrentName = inputName;
        DialogueManager.PlayerGender = selectedGender;

        Debug.Log("[이름 저장됨] " + inputName);
        Debug.Log("[성별 선택됨] " + selectedGender.ToString());

        nameInputPanel.SetActive(false);

        // 대사 시작
        DialogueManager.Instance.PlayScene(DialogueManager.Instance.currentSceneList[0]);
    }
}
