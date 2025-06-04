import re
from pathlib import Path
import json

# txt 파일 자동 감지
txt_files = list(Path(".").glob("*.txt"))
if not txt_files:
    raise FileNotFoundError("현재 폴더에 .txt 파일이 없습니다.")
elif len(txt_files) > 1:
    raise RuntimeError(".txt 파일이 여러 개 있습니다. 하나만 남겨 주세요.")

input_path = txt_files[0]
output_path = Path(".") / (input_path.stem + "_converted.json")


# 이름 라벨 자동 매핑
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
    }.get(speaker, (speaker, speaker))


# 트레잇 파싱
def parse_traits(trait_str):
    traits = []
    for part in trait_str.split(","):
        part = part.strip()
        if "+" in part:
            trait, val = part.split("+")
            traits.append(
                {"trait": trait.strip(), "value": int(val.strip()), "mode": "add"}
            )
        elif "*" in part:
            trait, val = part.split("*")
            traits.append(
                {
                    "trait": trait.strip(),
                    "value": float(val.strip()),
                    "mode": "multiply",
                }
            )
    return traits


# 후속 대사 처리: 리스트 저장 + 자동 이름 라벨까지 ㅠㅠ
def process_followup_lines(lines):
    full_text = "\n".join(lines)
    blocks = [part.strip() for part in full_text.split("|") if part.strip()]
    result = []
    for line in blocks:
        if ":" in line:
            speaker_raw, content = line.split(":", 1)
            speaker_raw = speaker_raw.strip()
            if speaker_raw.lower() in ["player", "플레이어"]:
                label_kr = "{playerName}"
            else:
                label_kr, _ = get_name_label(speaker_raw)
            result.append(f"{label_kr}: {content.strip()}")
        else:
            result.append(f"나레이션: {line.strip()}")
    return result


with open(input_path, "r", encoding="utf-8") as f:
    lines = f.read().splitlines()


# 파싱 시작
with open(input_path, "r", encoding="utf-8") as f:
    lines = f.read().splitlines()

scenes = []
current_scene = None
choice_counter = 0
choice_letters = ["A", "B", "C", "D"]

# followUp 멀티라인 수집용 변수
collecting_followup = False
current_followup_id = None
followup_lines = []
pending_followups = {}

for line in lines:
    line = line.strip()
    if not line or line.startswith("//"):
        continue

    if line.startswith("[SceneID:"):
        if collecting_followup and current_followup_id:
            pending_followups[current_followup_id] = process_followup_lines(
                followup_lines
            )
            collecting_followup = False
            followup_lines = []
        if current_scene:
            scenes.append(current_scene)
        scene_id = line.split(":")[1].strip("] ")
        current_scene = {"sceneId": scene_id, "dialogues": [], "choices": []}
        choice_counter = 0
        continue

    if line.startswith("Choice["):
        if collecting_followup and current_followup_id:
            pending_followups[current_followup_id] = process_followup_lines(
                followup_lines
            )
            collecting_followup = False
            followup_lines = []
        match = re.match(r"Choice\[(.+?)\]:\s*(.+?)\s*\((.+?)\)", line)
        if match:
            raw_cid, text, traits = match.groups()
            cid = (
                raw_cid
                if raw_cid.startswith(current_scene["sceneId"])
                else f"{current_scene['sceneId']}_{raw_cid}"
            )
            choice_obj = {
                "id": cid,
                "text_kr": text.strip(),
                "effects": parse_traits(traits),
                "followUp_kr": [],
                "followUp_fr": [],
            }
            if cid in pending_followups:
                choice_obj["followUp_kr"] = pending_followups.pop(cid)
            current_scene["choices"].append(choice_obj)
        continue

    if line.startswith("Choice:"):
        if collecting_followup and current_followup_id:
            pending_followups[current_followup_id] = process_followup_lines(
                followup_lines
            )
            collecting_followup = False
            followup_lines = []
        match = re.match(r"Choice:\s*(.+?)\s*\((.+?)\)", line)
        if match and current_scene:
            text, trait_str = match.groups()
            letter = (
                choice_letters[choice_counter]
                if choice_counter < 4
                else chr(65 + choice_counter)
            )
            cid = f"{current_scene['sceneId']}_{letter}"
            choice_obj = {
                "id": cid,
                "text_kr": text.strip(),
                "effects": parse_traits(trait_str),
                "followUp_kr": [],
                "followUp_fr": [],
            }
            if cid in pending_followups:
                choice_obj["followUp_kr"] = pending_followups.pop(cid)
            current_scene["choices"].append(choice_obj)
            choice_counter += 1
        continue

    if re.match(r"FollowUp(\[\w+])?:", line):
        if collecting_followup and current_followup_id:
            pending_followups[current_followup_id] = process_followup_lines(
                followup_lines
            )
        match_full = re.match(r"FollowUp\[(.+?)\]:?", line)
        if match_full:
            current_followup_id = match_full.group(1)
        else:
            idx = line.replace("FollowUp", "").replace(":", "").strip()
            if idx.isdigit() and current_scene and choice_counter > int(idx) - 1:
                current_followup_id = current_scene["choices"][int(idx) - 1]["id"]
            else:
                current_followup_id = None
        followup_lines = []
        collecting_followup = current_followup_id is not None
        continue

    if collecting_followup:
        if (
            line.startswith("Choice")
            or line.startswith("[SceneID:")
            or line.startswith("FollowUp")
        ):
            pending_followups[current_followup_id] = process_followup_lines(
                followup_lines
            )
            current_followup_id = None
            followup_lines = []
            collecting_followup = False
        else:
            followup_lines.append(line)
        continue

    if ":" in line:
        speaker, text = line.split(":", 1)
        speaker = speaker.strip()
        label_kr, label_fr = get_name_label(speaker)
        current_scene["dialogues"].append(
            {
                "speaker": speaker,
                "label_kr": label_kr,
                "label_fr": label_fr,
                "text_kr": text.strip(),
                "text_fr": "",
            }
        )

# ✅ 마지막 followUp 보완
if collecting_followup and current_followup_id:
    pending_followups[current_followup_id] = process_followup_lines(followup_lines)
if current_scene:
    scenes.append(current_scene)
    
# 저장
with open(output_path, "w", encoding="utf-8") as f:
    json.dump(scenes, f, ensure_ascii=False, indent=2)

print("✅ 변환 완료: JSON 파일 생성 완!!!")
