# Structure initiale du projet **feminist育成-sim**

Les documents de conception et les fichiers de dialogues multilingues sont regroupés dans le dossier `Docs`.  

```plaintext
feminist-sim/
├── Assets/
│   ├── Scenes/                  ← 씬 파일 / Scènes Unity (.unity)
│
│   ├── Scripts/
│   │   ├── Core/                ← 게임 시작, 메인 루프, 상태 관리
│   │   ├── DialogueSystem/      ← JSON 파싱, 대사 진행, 선택지 처리
│   │   ├── UI/                  ← 텍스트 박스, 버튼, 선택지 생성
│   │   ├── Character/           ← 캐릭터 제어, 애니메이션
│   │   ├── Logic/               ← 성향 누적, 조건 분기, 엔딩 판단 등
│   │   └── Utils/               ← 공통 유틸 함수 (ex: string 파싱, 로깅)
│
│   ├── Resources/
│   │   ├── DialogueData/          ← JSON 대사 스크립트 / Scripts de dialogue
│   │   │   ├── MainStory/         ← 메인 스토리 챕터별 JSON
│   │   │   ├── SideEvents/        ← 선택지 이벤트, 소규모 갈등
│   │   │   ├── System/            ← 튜토리얼, 점성술사, 엔딩 조건
│   │   │   └── Localization/      ← 언어별 텍스트 JSON (kr, fr)
│   │   ├── CharacterProfiles/     ← NPC 성향, 관계도 JSON
│   │   ├── StatusTables/          ← 성향 점수 누적용 DB
│   │   └── Configs/               ← 게임 설정값 (텍스트 속도, UI 사이즈 등)
│
│   ├── UI/                      ← 텍스트 박스, 선택지 버튼, 애니메이션 프리팹  
│   │                            ← UI : boîtes de texte, boutons, animations
│
│   ├── Art/
│   │   ├── Characters/          ← 로우폴리 FBX/Prefab  
│   │   │                        ← Modèles low-poly (FBX/Prefab)
│   │   ├── Backgrounds/         ← 배경 PNG 혹은 큐브 맵  
│   │   │                        ← Arrière-plans (PNG ou cubemaps)
│   │   └── Icons/               ← UI 아이콘 등  
│   │                            ← Icônes pour l'interface
│
│   ├── Prefabs/                 ← 전체 씬 구성용 프리팹  
│   │                            ← Prefabs de mise en scène
│
│   └── Fonts/                   ← TextMeshPro용 폰트  
│                                ← Polices pour TextMeshPro
│
├── Docs/                        ← 기획 문서, 스토리 구성, 다국어 대사 번역  
│                                ← Documents de conception, scénarios, traductions
│
│   ├── Scenario/                ← 스토리, 선택지 설계용 문서  
│   │                            ← Scripts narratifs, structure de choix
│
│   ├── Localization_kr/         ← 한국어 대사 원본  
│   │                            ← Scripts originaux en coréen
│
│   ├── Localization_fr/         ← 프랑스어 번역본  
│   │                            ← Traductions en français
│
│   └── Mechanics/               ← 성향 구조, 시스템 설계서 등  
│                                ← Systèmes de gameplay, structures de valeurs
│
├── Packages/
├── ProjectSettings/
├── README.md
├── .gitignore
└── .gitattributes
```
