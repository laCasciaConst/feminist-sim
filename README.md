Voici la structure initiale du projet **feminist育成-sim**.  
Les documents de conception et les fichiers de dialogues multilingues sont regroupés dans le dossier `Docs`.

```plaintext
feminist育成-sim/
├── Assets/
│   ├── Scenes/                  ← 씬 파일 (.unity)  
│                                ← Scènes Unity (.unity)
│
│   ├── Scripts/
│   │   └── Dialogue/            ← JSON 파서, 선택지 시스템 등  
│                                ← Parseur JSON, logique de choix
│
│   ├── Resources/
│   │   └── DialogueData/        ← JSON 대사 스크립트  
│                                ← Scripts de dialogue au format JSON
│
│   ├── UI/                      ← 텍스트 박스, 선택지 버튼, 애니메이션 프리팹  
│                                ← UI : boîtes de texte, boutons, animations
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
│                                ← Prefabs de mise en scène
│
│   └── Fonts/                   ← TextMeshPro용 폰트  
│                                ← Polices pour TextMeshPro
│
├── Docs/                        ← 기획 문서, 스토리 구성, 다국어 대사 번역  
│                                ← Documents de conception, scénarios, traductions
│
│   ├── Scenario/                ← 스토리, 선택지 설계용 문서  
│                                ← Scripts narratifs, structure de choix
│
│   ├── Localization_kr/         ← 한국어 대사 원본  
│                                ← Scripts originaux en coréen
│
│   ├── Localization_fr/         ← 프랑스어 번역본  
│                                ← Traductions en français
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