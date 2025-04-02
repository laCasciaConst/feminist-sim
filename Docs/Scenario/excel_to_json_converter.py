
import pandas as pd
import json

# 엑셀 파일 불러오기
df = pd.read_excel("ScriptTemplate_GameDialogue.xlsx")

dialogue_block = []
choices = []

for _, row in df.iterrows():
    if row['Type'] == 'Dialogue':
        dialogue_block.append({
            "Speaker": row['Speaker'],
            "Emotion": row['Emotion'],
            "Dialogue": row['Dialogue']
        })
    elif row['Type'] == 'Choice':
        choices.append({
            "id": row['ChoiceID'] + "_" + str(len(choices)+1),
            "text": row['ChoiceText'],
            "effect": row['Effect'],
            "availableIf": row['AvailableIf'],
            "lockedIf": row['LockedIf']
        })

output = [{
    "SceneID": df.iloc[0]['SceneID'],
    "DialogueBlock": dialogue_block,
    "ChoiceGroup": {
        "ChoiceID": choices[0]["id"].split("_")[0],
        "Choices": choices
    }
}]

# JSON 저장
with open("scene_output.json", "w", encoding="utf-8") as f:
    json.dump(output, f, ensure_ascii=False, indent=2)

print("✅ JSON 변환 완료: scene_output.json")
