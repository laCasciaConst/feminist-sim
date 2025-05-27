import json
from pathlib import Path

# 원본 및 팔로우업 JSON 파일 탐색
original_file = None
followup_file = None

for file in Path(".").glob("*.json"):
    if file.stem.endswith("_original"):
        original_file = file
    elif file.stem.endswith("_followups_only"):
        followup_file = file

if not original_file or not followup_file:
    raise FileNotFoundError("두 파일이 모두 있어야 합니다: *_original.json 와 *_followups_only.json")

# 파일 로드
with open(original_file, encoding="utf-8") as f:
    original_data = json.load(f)

with open(followup_file, encoding="utf-8") as f:
    followup_data = json.load(f)

# followUp 병합
for scene in original_data:
    for choice in scene.get("choices", []):
        cid = choice["id"]
        if cid in followup_data:
            choice["followUp_kr"] = followup_data[cid]
            choice["followUp_fr"] = []

# 저장
merged_output = original_file.stem.replace("_original", "_merged.json")
with open(merged_output, "w", encoding="utf-8") as f:
    json.dump(original_data, f, ensure_ascii=False, indent=2)

merged_output
print("✅ 머지 완료료.")
