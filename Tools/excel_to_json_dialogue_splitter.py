import os
import pandas as pd
import json

# 대상 폴더
base_dir = "/mnt/data/"
excel_files = [f for f in os.listdir(base_dir) if f.endswith(".xlsx")]

# 결과 저장 경로 목록
output_files = []

for excel_file in excel_files:
    excel_path = os.path.join(base_dir, excel_file)
    basename = os.path.splitext(excel_file)[0]

    df = pd.read_excel(excel_path)

    main_story = {
        "SceneID": df["SceneID"].iloc[0],
        "DialogueBlock": []
    }
    localization_kr = {}
    localization_fr = {}

    current_dialogue = None

    for i, row in df.iterrows():
        if row["Type"] == "Dialogue":
            dialogue_id = f"d{i+1:03}"
            current_dialogue = {
                "id": dialogue_id,
                "Speaker": row["Speaker"],
                "Emotion": row["Emotion"],
                "Choices": []
            }
            main_story["DialogueBlock"].append(current_dialogue)
            localization_kr[dialogue_id] = row["Dialogue_kr"]
            localization_fr[dialogue_id] = row["Dialogue_fr"]
        elif row["Type"] == "Choice" and current_dialogue is not None:
            choice_index = len(current_dialogue["Choices"])
            choice_id = f"{row['ChoiceID']}_{chr(65 + choice_index)}"
            choice_data = {
                "id": choice_id,
                "effect": row["Effect"],
                "availableIf": row["AvailableIf"] if pd.notna(row["AvailableIf"]) else "",
                "lockedIf": row["LockedIf"] if pd.notna(row["LockedIf"]) else ""
            }
            current_dialogue["Choices"].append(choice_data)
            localization_kr[choice_id] = row["ChoiceText_kr"]
            localization_fr[choice_id] = row["ChoiceText_fr"]

    main_path = os.path.join(base_dir, f"MainStory_{basename}.json")
    kr_path = os.path.join(base_dir, f"Localization_kr_{basename}.json")
    fr_path = os.path.join(base_dir, f"Localization_fr_{basename}.json")

    with open(main_path, "w", encoding="utf-8") as f:
        json.dump(main_story, f, ensure_ascii=False, indent=2)
    with open(kr_path, "w", encoding="utf-8") as f:
        json.dump(localization_kr, f, ensure_ascii=False, indent=2)
    with open(fr_path, "w", encoding="utf-8") as f:
        json.dump(localization_fr, f, ensure_ascii=False, indent=2)

    output_files.append((main_path, kr_path, fr_path))

output_files

print("✅ JSON 파일 3개 생성 완료!")
