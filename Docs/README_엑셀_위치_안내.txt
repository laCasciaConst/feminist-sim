
✅ 엑셀 파일 위치 안내 (Docs용)

엑셀 원본 파일은 다음 위치에 보관하세요:
📂 Docs/Localization_kr/    ← 한국어 중심 작성
📂 Docs/Localization_fr/    ← 프랑스어 번역 입력

다국어 병합 관리용 파일은 다음에 둡니다:
📂 Docs/Scenario/

예: 
Docs/Scenario/scene_part_2_2_gangnam_multilang.xlsx

이후 Python 변환 스크립트는 이 파일을 읽어
언어별 JSON으로 나눠서 저장합니다:
→ Assets/Resources/DialogueData/Localization/kr/...
→ Assets/Resources/DialogueData/Localization/fr/...
