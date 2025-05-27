# FollowUp이 한 줄에 내용까지 포함된 경우도 파싱하는 버전
from pathlib import Path
import re
import json
from typing import Union

txt_files = list(Path(".").glob("*.txt"))
txt_file: Union[Path, None] = txt_files[0] if txt_files else None
if not txt_files:
    raise FileNotFoundError("현재 폴더에 .txt 파일이 없습니다.")

input_path = txt_files[0]
output_path = input_path.stem + "_followups_only.json"

followups = {}
collecting = False
current_id = None
buffer = []
scene_id = ""
auto_index = 0  # A, B, C, D


def get_name_label(speaker):
    return {
        "Player": ("당신", "Toi"),
        "Jin": ("진이", "Jin"),
        "Jun": ("준이", "Jun"),
        "FriendA": ("친구 A", "Ami A"),
        "FriendB": ("친구 B", "Ami B"),
        "FriendC": ("친구 C", "Ami C"),
        "Mom": ("어머니", "Mère"),
        "Dad": ("아버지", "Père"),
        "Narration": ("내레이션", "Narration"),
        "Teacher": ("담임", "Prof principal"),
        "TeacherB": ("교사 B", "Prof B"),
        "TeacherC": ("교사 C", "Prof C"),
        "Boyfriend": ("남자친구", "Petit ami"),
    }.get(speaker.strip(), speaker.strip())


def process_lines(lines):
    joined = "\n".join(lines)
    chunks = [part.strip() for part in joined.split("|") if part.strip()]
    result = []
    for line in chunks:
        if ":" in line:
            speaker, content = line.split(":", 1)
            label = get_name_label(speaker)
            result.append(f"{label}: {content.strip()}" if label else content.strip())
        else:
            result.append(line)
    return result


with open(str(txt_file), "r", encoding="utf-8") as f:
    for line in f:
        line = line.strip()
        if not line:
            continue

        if line.startswith("[SceneID:"):
            scene_id = line.split(":")[1].strip("] ")
            auto_index = 0  # reset A/B/C/D
            continue

        match = re.match(r"FollowUp\[(.+?)\]:(.*)", line)
        if match:
            if current_id:
                followups[current_id] = process_lines(buffer)
            current_id = match.group(1)
            inline = match.group(2).strip()
            buffer = [inline] if inline else []
            collecting = True
            continue

        # 새 기능: FollowUp: 자동 ID 부여
        if line.startswith("FollowUp:"):
            if current_id:
                followups[current_id] = process_lines(buffer)
            if scene_id:
                letter = chr(65 + auto_index)  # A, B, C, ...
                current_id = f"{scene_id}_{letter}"
                auto_index += 1
            else:
                current_id = f"UNKNOWN_{auto_index}"
                auto_index += 1
            inline = line[len("FollowUp:") :].strip()
            buffer = [inline] if inline else []
            collecting = True
            continue

        elif collecting and (
            line.startswith("Choice")
            or line.startswith("[SceneID:")
            or line.startswith("FollowUp")
        ):
            followups[current_id] = process_lines(buffer)
            current_id = None
            buffer = []
            collecting = False
        elif collecting:
            buffer.append(line)

if collecting and current_id:
    followups[current_id] = process_lines(buffer)

with open(output_path, "w", encoding="utf-8") as f:
    json.dump(followups, f, ensure_ascii=False, indent=2)

output_path
print("✅ 팔로우업만 있는 JSON 파일 만들기 끗.")
