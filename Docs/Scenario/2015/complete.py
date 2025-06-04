import subprocess
from pathlib import Path
import json
import shutil

# 1. 텍스트 파일 찾기
txt_files = list(Path(".").glob("*.txt"))
if not txt_files:
    raise FileNotFoundError("변환할 .txt 파일이 없습니다.")

txt_file = txt_files[0]
base = txt_file.stem

# 2. 변환 스크립트 실행
subprocess.run(["python", "convert.py"], check=True)
subprocess.run(["python", "followUp_only.py"], check=True)

# 3. 파일 이름 정리
converted_file = Path(f"{base}_converted.json")
followup_file = Path(f"{base}_followups_only.json")
complete_file = Path(f"{base}_complete.json")

# 4. 병합 수행
with open(converted_file, encoding="utf-8") as f:
    original_data = json.load(f)
with open(followup_file, encoding="utf-8") as f:
    followup_data = json.load(f)

for scene in original_data:
    for choice in scene.get("choices", []):
        cid = choice.get("id")
        if not cid:
            continue
        if "availableIf" not in choice:
            choice["availableIf"] = ""
        if "lockedIf" not in choice:
            choice["lockedIf"] = ""
        if cid in followup_data:
            choice["followUp_kr"] = followup_data[cid]
            choice["followUp_fr"] = [] 

with open(complete_file, "w", encoding="utf-8") as f:
    json.dump(original_data, f, ensure_ascii=False, indent=2)

# 5. nn/ 폴더 생성 후 파일 이동
output_dir = Path(base)
output_dir.mkdir(exist_ok=True)

shutil.move(str(txt_file), output_dir / txt_file.name)
shutil.move(str(converted_file), output_dir / converted_file.name)
shutil.move(str(followup_file), output_dir / followup_file.name)

# 최종 완료 메시지
print("✅ 완료: _complete.json 생성 ><")

complete_file
