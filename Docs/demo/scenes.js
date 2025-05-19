// scenes.js

const scenes_kr = [
  {
    id: "2015-A-1",
    text: `이번 학기 동아리는 반드시 하나 이상 신청해야 합니다.\\

  \\친구A: 너 원예 동아리 갔다며? 야, 남자가 거길 왜 가?
  \\형: 남은 데가 거기밖에 없어서 그냥 신청했는데…
  \\친구B: 막 정원에 꽃 심고 그러나?
  \\친구A: 얘들아! 준이가 꽃꽂이 한대!
  \\형: 남는 자리가 없었단 말이야!`,
    choices: [
      {
        text: "그게 뭐 어때서.",
        effect: { individualism: 1, equality: 1 },
        followup: `\\플레이어: 별로 상관 없지 않아? 그리고, 정원에서 딴짓해도 모르실 걸.
  \\형: 그런가?`,
      },
      {
        text: "다른 사람과 바꾸자!",
        effect: { authority: 1 },
        followup: `플레이어: 선생님께 부탁하면 다른 동아리에 들어갈 수 있을지도 몰라.
  \\형: 그런가?`,
      },
      {
        text: "지루하겠다.",
        effect: { individualism: 2, collective: 1 },
        followup: `플레이어: 거긴 여자애들 뿐이잖아. 친구도 없고.
  \\형: (속상해 보인다.)`,
      },
      {
        text: "(관심 없음.)",
        effect: { cynicism: 1 },
        followup: `플레이어: (준이 알아서 하겠지.)
  \\형: (속상해 보인다.)`,
      },
    ],
  },
  {
    id: "2015-A-2",
    text: `누나: 있지, 급식실 선생님이 남자애들한테만 반찬을 많이 주는 거 같애! 내가 지켜봤는데 소세지를 엄청 많이 받았어. 너도 봤어?`,
    choices: [
      {
        text: "사람마다 정해진 양이 있는 걸.",
        effect: { authority: 1 },
        followup: `플레이어: 너무 신경쓰지 마. 우연이겠지.
  \\누나: 그런가…`,
      },
      {
        text: "우리도 더 달라고 하자.",
        effect: { individualism: 2 },
        followup: `플레이어: 내 거 더 먹을래?
  \\누나: 고마워. 근데 더 먹고 싶어서 그런 거 아니야. 그냥 그렇다구!`,
      },
      {
        text: "그 선생님, 이상하네.",
        effect: { equality: 2, collective: 4 },
        followup: `누나: 그렇지? 정말 이상해!`,
      },
      {
        text: "나는 몰라.",
        effect: { cynicism: 1 },
        followup: `플레이어: 착각 아닐까?
  \\누나: 흠…`,
      },
    ],
  },
  {
    id: "2015-A-3",
    text: `(장면: 집, 저녁 시간. 누나가 친구들이랑 영상통화 중)

\\친구A: 야 너 교복 치마 계속 입을 거야? 이번에 바지 입고 온 애 벌점 먹었대.
\\누나: 진짜? 근데 치마가 더 예쁘긴 하잖아…
\\친구B: 예쁘고 말고를 떠나서 너무 불편해. 밥 먹고 나면 배가 불편해.
\\친구A: 그냥 바지랑 치마 중에 고를 수 있으면 안 돼?
\\누나: 난 벌 받기 싫어!

\\내가 생각하기에는…`,
    choices: [
      {
        text: "맞아. 바지보다 치마가 더 귀여워.",
        effect: { individualism: 3 },
        followup: `\\플레이어: 맞아. 바지보다 치마가 더 귀여워. 바지 교복은 별로 안 예뻐.`,
      },
      {
        text: "규칙은 어쩔 수 없지.",
        effect: { authority: 2 },
        followup: `\\플레이어: 규칙은 어쩔 수 없지. 벌 받고 싶지 않으니까.`,
      },
      {
        text: "체육복 바지를 입자!",
        effect: { collective: 2, equality: 2 },
        followup: `\\플레이어: 체육복 바지를 입자! 교복 체육복은 편하니까 괜찮아.`,
      },
      {
        text: "잘 모르겠어.",
        effect: { cynicism: 1 },
        followup: `\\플레이어: 잘 모르겠어. 그런 건 중요치 않다.`,
      },
    ],
  },
  {
    id: "2015-B-1",
    text: `(반 대항전 체육대회)
\\(장면: 저녁. 누나가 형과 이야기 중)
\\
\\형: 맞다, 이번 대회 1반 남자애들이랑 우리 반이랑 축구 붙는데 걔네 수비 잘하더라.
\\누나: 아 맞다. 우리는 2반이랑 붙는대. 근데 난 싫어.
\\형: 그게 왜? 너희 반에 피구 잘 하는 여자애들 많지 않아?
\\누나: 그건 맞아. 다들 잘해.
\\형: 차라리 다행이네! 여자 피구랑 남자 축구, 한 쪽만 이겨도 반 전체 피자래.
\\누나: 아니 그게, 나 농구하고 싶었는데…
\\
\\진이는 피구 대신 농구가 하고 싶었던 모양이다.`,
    choices: [
      {
        text: "그래도 잘하잖아!",
        effect: { authority: 1 },
        followup: `\\누나: 뭐 어쩔 수 없지. 농구 할 줄 아는 여자애들이 많이 없더라.
\\형: 그건 그래. 어쨌든 무조건 피자 먹을 거야!
\\플레이어: 피자에 콜라!
\\형: 뭐? 몬스터를 마셔야지!
\\누나: 넌 물을 좀 마셔. 그러다 키 안 큰다.`,
      },
      {
        text: "피구도 재밌어!",
        effect: { cynicism: 1, collective: 1 },
        followup: `\\누나: 뭐 어쩔 수 없지. 농구 할 줄 아는 여자애들이 많이 없더라.
\\형: 그건 그래. 어쨌든 무조건 피자 먹을 거야!
\\플레이어: 피자에 콜라!
\\형: 뭐? 몬스터를 마셔야지!
\\누나: 넌 물을 좀 마셔. 그러다 키 안 큰다.`,
      },
      {
        text: "이기면 피자잖아!",
        effect: { authority: 2, cynicism: 1 },
        followup: `\\누나: 뭐 어쩔 수 없지. 농구 할 줄 아는 여자애들이 많이 없더라.
\\형: 그건 그래. 어쨌든 무조건 피자 먹을 거야!
\\플레이어: 피자에 콜라!
\\형: 뭐? 몬스터를 마셔야지!
\\누나: 넌 물을 좀 마셔. 그러다 키 안 큰다.`,
      },
      {
        text: "아쉽겠다.",
        effect: { equality: 4 },
        followup: `\\누나: 뭐 어쩔 수 없지. 농구 할 줄 아는 여자애들이 많이 없더라.
\\형: 그건 그래. 어쨌든 무조건 피자 먹을 거야!
\\플레이어: 피자에 콜라!
\\형: 뭐? 몬스터를 마셔야지!
\\누나: 넌 물을 좀 마셔. 그러다 키 안 큰다.`,
      },
    ],
  },
  {
    id: "2015-B-2",
    text: `(장면: 형과 친구가 학교에서 이야기 중. 플레이어는 옆에서 듣고 있음)
\\
\\형: 남자는 왜 탈의실 없어? 오늘도 체육 끝나고 복도에서 갈아입었잖아.
\\친구: 여자애들은 탈의실 있지?
\\형: 어. 걔네는 문 잠그고 갈아입는데 우리는 그냥 “야 빨리 갈아입어~” 이럼.
\\친구: 선생님 말로는 남자 탈의실 만들 공간이 없대. 근데 별 상관 없잖아.
\\형: 상관 없다고? 자꾸 그러면 여기서 벗는다?
\\친구: 난 별로 안 부끄러운데. 너 되게 예민하네?`,
    choices: [
      {
        text: "남자를 누가 봐?",
        effect: { cynicism: 3 },
        followup: `\\형: 난 속옷 보여주기 싫어.
\\친구: 아, 남자는 괜찮아! 다들 여름에 더우면 웃통 까고 축구하잖아.
\\형: 난 그런 거 안 해!
\\친구: 진짜 소심하네.`,
      },
      {
        text: "화장실에서 갈아입으면 되지!",
        effect: { individualism: 2 },
        followup: `\\형: 난 속옷 보여주기 싫어.
\\친구: 아, 남자는 괜찮아! 다들 여름에 더우면 웃통 까고 축구하잖아.
\\형: 난 그런 거 안 해!
\\친구: 진짜 소심하네.`,
      },
      {
        text: "공간이 없으면 어쩔 수 없지.",
        effect: { authority: 2 },
        followup: `\\형: 난 속옷 보여주기 싫어.
\\친구: 아, 남자는 괜찮아! 다들 여름에 더우면 웃통 까고 축구하잖아.
\\형: 난 그런 거 안 해!
\\친구: 진짜 소심하네.`,
      },
      {
        text: "복도에서? 별로다.",
        effect: { equality: 2 },
        followup: `\\형: 난 속옷 보여주기 싫어.
\\친구: 아, 남자는 괜찮아! 다들 여름에 더우면 웃통 까고 축구하잖아.
\\형: 난 그런 거 안 해!
\\친구: 진짜 소심하네.`,
      },
    ],
  },
  {
    id: "2015-B-3",
    text: `(장면: 집. 주말 오후)
\\
  \\형: 뭐야, 누나는 왜 용돈 만원 더 줘?
  \\누나: 나는 화장품도 사야 돼. 애들이 다 하니까 나만 안 하면 좀 그래.
  \\엄마: 원래 여자들은 이것저것 돈 들 일이 많아.
  \\(플레이어는 옆에서 듣고 있음)`,
    choices: [
      {
        text: "불공평해.",
        effect: { authority: 2 },
        followup: `플레이어: 나도 살 거 있는데!
  \\형: 봐봐, 나만 그런 생각하는 거 아님.
  \\누나: (입 다문다)`,
      },
      {
        text: "나도 만 원 더 받자!",
        effect: { authority: 1 },
        followup: `플레이어: 그럼 나도 만 원 더 줘.
  \\엄마: (웃으며) 넌 뭘 살 건데?
  \\형: (헛웃음)`,
      },
      {
        text: "화장품 안 쓰니까 괜찮잖아.",
        effect: { collective: 2, cynicism: 2 },
        followup: `누나: 맞아. 너는 안 해도 되잖아.
  \\형: (시큰둥)`,
      },
    ],
  },
  {
    id: "2015-C-1",
    text: `가족들이 각자 스마트폰을 보고 있는 거실
 \\ 누나: 무명 여배우가 유명 남배우한테 성추행 당했다고 고소했대
  \\형: 또야?
  \\누나: 근데 그 배우는 여성재단에 기부도 했었대. 댓글 난리났어.
  \\형: 이제 누가 진짠지도 모르겠다. 무섭다 진짜.

  \\
  \\플레이어 여성: 「한남이 또 한남함」
「아니 상식적으로 이게 관심받으려고 거짓말한 거겟냐고요 사람들아」
「좋아한걸 쪽팔리게 하는 놈들은 죽어야한다…」
「근데 그 배우 평소엔 이미지 좋았잖아 이제 누굴 믿음? ㅠㅠ」
\\
\\플레이어 남성: 「무고죄 드가자」
「형이 딱 정리해준다 무명 여배우 뜨려고 판 키운거다 믿은 놈은 옥장판 사라」
「저여자 19금영화 찍음. 믿을만한 건 아닌듯」
「상식적으로 이걸 믿냐?」
「중립기어 박음」
한녀들 일할생각을 해야지 꽃뱀짓해서 돈 벌 잘못된생각을 하네
요즘은 고소 당할까봐 여자랑 섹스도 함부로 못하겠다
한국 남자들이 펜스룰치니까 무고 빌드업하노ㅋㅋ
`,
    choices: [
      {
        text: "믿어줄 수밖에 없잖아.",
        effect: { equality: 3, collective: 2 },
        followup: `플레이어: 여자가 저렇게 나섰으면, 뭐가 있었겠지.
  \\누나: 나도 그렇게 생각했어.
  \\형: 그건 모르는 일이야.`,
      },
      {
        text: "수사 결과를 기다리자.",
        effect: { authority: 2 },
        followup: `플레이어: 재판 결과에서 밝혀질 거야.`,
      },
      {
        text: "나는 잘 모르겠어.",
        effect: { cynicism: 2, individualism: 2 },
        followup: `플레이어: 누가 맞는지 모르겠는데. 그냥 헷갈린다.`,
      },
      {
        text: "그럴만한 이유가 있겠지.",
        effect: { cynicism: 1, individualism: 2 },
        followup: `누나: (정적) 그 말, 누구한테 하는 건데?
  \\형: (피식) 알아보는 눈이 있어야지.`,
      },
    ],
  },
  {
    id: "2015-C-2",
    text: `(장면: 저녁. 누나가 노트북으로 커뮤니티 댓글 보며 웃는다)
\\
\\플레이어: 뭐 봐?
\\형: 또 남자 까는 글이야?
\\누나: 댓글에 “역시 한남충 클라스ㅋㅋ” 이러던데? 너무 찰지지 않아?
\\형: 진짜… 이젠 남자면 다 쓰레기냐?
\\(플레이어는 화면을 같이 본다. ‘한남충’, ‘한남들 특징.txt’ 같은 글들이 올라온다.)`,
    choices: [
      {
        text: "이거 너무 심한 거 아냐?",
        effect: { authority: 2 },
        followup: `플레이어: 아무리 그래도 이런 말은 좀…
  \\형: (고개 끄덕)
  \\누나: 듣기 싫으면 나쁜 짓을 안 하면 되지!`,
      },
      {
        text: "웃긴 건 웃긴 거지.",
        effect: { individualism: 2, cynicism: 1 },
        followup: `플레이어: 나도 터졌네ㅋㅋㅋ
  \\형: (한숨)
  \\누나: (같이 웃는다)`,
      },
      {
        text: "진짜 다 저런 댓글이야?",
        effect: { collective: 4 },
        followup: `플레이어: 좀 무섭다, 진짜로…
  \\누나: (잠깐 정색) 무서워할 건 아니지! 웃기잖아.`,
      },
      {
        text: "지업자득이야!",
        effect: { cynicism: 2, equality: 2 },
        followup: `플레이어: 한남충… 솔직히 어울리는 사람 꽤 되긴 해.
  \\형: (불쾌하게 쳐다봄)
  \\누나: (재밌다는 듯)`,
      },
    ],
  },
];

const scenes_fr = [
  {
    id: "2015-A-1",
    text: `Ce semestre, tout le monde doit s'inscrire à au moins un club.
  \\
  \\Ami A : T'es allé au club de jardinage ? Sérieux, pourquoi un mec irait là-bas ?
  \\Frère : Y avait plus de place ailleurs, alors j’ai juste pris ce qui restait…
  \\Ami B : Ils plantent des fleurs ou quoi là-bas ?
  \\Ami A : Eh les gars ! Jun fait de l’arrangement floral !
  \\Frère : Je vous dis qu’y avait plus de place !`,
    choices: [
      {
        text: "Et alors ?",
        effect: { individualism: 1, equality: 1 },
        followup: `\\Joueur·se : Et alors ? On s'en fiche. En plus, tu peux glander dans le jardin, personne verra.
  \\Frère : Tu crois ?`,
      },
      {
        text: "Demande à changer de club !",
        effect: { authority: 1 },
        followup: `\\Joueur·se : Si tu demandes au prof, t’auras peut-être une autre place.
  \\Frère : Tu crois ?`,
      },
      {
        text: "Quel ennui, ce club...",
        effect: { individualism: 2, collective: 1 },
        followup: `\\Joueur·se : Y a que des filles là-bas. Tu vas t’ennuyer.
  \\Frère : (semble déçu)`,
      },
      {
        text: "M'en fiche...",
        effect: { cynicism: 1 },
        followup: `\\Joueur·se : Il gère, non ?
  \\Frère : (semble déçu)`,
      },
    ],
  },
  {
    id: "2015-A-2",
    text: `Soeur : Hé, j’ai l’impression que les gars reçoivent plus de bouffe à la cantine ! J’ai bien regardé, tu trouves pas que la dame de cantine donne plus à certains garçons ? Tu l’as pas vu ?`,
    choices: [
      {
        text: "Y a une portion prévue pour chacun.",
        effect: { authority: 1 },
        followup: `\\Joueur·se : T’inquiète pas. C’est sûrement un hasard.
  \\Soeur : Ah bon…`,
      },
      {
        text: "On demande plus aussi !",
        effect: { individualism: 2 },
        followup: `\\Joueur·se : Tu veux le mien ?
  \\Soeur : Merci. Mais c’est pas que j’ai encore faim, c’est juste… tu vois ?`,
      },
      {
        text: "Cette dame-là est chelou…",
        effect: { equality: 2, collective: 4 },
        followup: `\\Soeur : Hein, t’as vu ? Vraiment bizarre ! Elle donne jamais autant à moi.`,
      },
      {
        text: "J’en sais rien.",
        effect: { cynicism: 1 },
        followup: `\\Joueur·se : C’est peut-être toi qui t’imagines des trucs.
  \\Soeur : Hm…`,
      },
    ],
  },
  {
    id: "2015-A-3",
    text: `(Scène : le soir, chez eux. Soeur est en appel vidéo avec ses amies)
    \\Amie A : Tu vas continuer à mettre **la jupe de l’uniforme** ? Une fille qui est venue en pantalon a pris une retenue.
    \\Soeur : Sérieux ? Mais la jupe est quand même plus jolie…
    \\Amie B : Jolie ou pas, c’est super inconfortable. Surtout après manger.
    \\Amie A : On devrait juste pouvoir choisir entre la jupe et le pantalon, non ?
    \\Soeur : Moi j’veux pas avoir de punition !
    \\
    \\Joueur·se pense...`,
    choices: [
      {
        text: "La jupe est plus mignonne.",
        effect: { individualism: 3 },
        followup: `\\Joueur·se : Franchement, les pantalons d’uniforme sont pas très beaux.`,
      },
      {
        text: "C’est la règle, pas le choix.",
        effect: { authority: 2 },
        followup: `\\Joueur·se : Autant éviter les punitions.`,
      },
      {
        text: "Pourquoi pas un pantalon de sport ?",
        effect: { collective: 2, equality: 2 },
        followup: `\\Joueur·se : C’est plus confortable. Et ça passe, non ?`,
      },
      {
        text: "Je sais pas trop...",
        effect: { cynicism: 1 },
        followup: `\\Joueur·se : Pas sûr que ce soit très important.`,
      },
    ],
  },
  {
    id: "2015-B-1",
    text: `(Tournoi inter-classes)
\\(Soir. Soeur parle avec le frère)
\\
\\Frère : Eh, notre classe joue contre les gars de 1ère. Ils sont bons en défense.
\\Soeur : Ah oui, et nous on joue contre la 2e. J’aime pas.
\\Frère : Pourquoi ? Les filles de ta classe sont fortes au ballon prisonnier, non ?
\\Soeur : C’est vrai. Elles sont douées.
\\Frère : Tant mieux ! Si l’une des équipes gagne, on a une pizza pour toute la classe.
\\Soeur : Non mais moi je voulais faire du basket...
\\
\\Apparemment, Jin voulait faire du basket au lieu du ballon prisonnier.`,
    choices: [
      {
        text: "Vous êtes fortes quand même !",
        effect: { authority: 1 },
        followup: `\\Soeur : Bon, pas le choix. Peu de filles savent jouer au basket.
\\Frère : C’est clair. De toute façon, on aura la pizza !
\\Joueur·se : Avec du coca !
\\Frère : Quoi ? Faut prendre un Monster !
\\Soeur : Bois de l’eau, tu vas pas grandir sinon.`,
      },
      {
        text: "Le ballon prisonnier, c’est fun aussi !",
        effect: { cynicism: 1, collective: 1 },
        followup: `\\Soeur : Bon, pas le choix. Peu de filles savent jouer au basket.
\\Frère : C’est clair. De toute façon, on aura la pizza !
\\Joueur·se : Avec du coca !
\\Frère : Quoi ? Faut prendre un Monster !
\\Soeur : Bois de l’eau, tu vas pas grandir sinon.`,
      },
      {
        text: "Faut gagner, y a une pizza !",
        effect: { authority: 2, cynicism: 1 },
        followup: `\\Soeur : Bon, pas le choix. Peu de filles savent jouer au basket.
\\Frère : C’est clair. De toute façon, on aura la pizza !
\\Joueur·se : Avec du coca !
\\Frère : Quoi ? Faut prendre un Monster !
\\Soeur : Bois de l’eau, tu vas pas grandir sinon.`,
      },
      {
        text: "C’est dommage pour le basket...",
        effect: { equality: 4 },
        followup: `\\Soeur : Bon, pas le choix. Peu de filles savent jouer au basket.
\\Frère : C’est clair. De toute façon, on aura la pizza !
\\Joueur·se : Avec du coca !
\\Frère : Quoi ? Faut prendre un Monster !
\\Soeur : Bois de l’eau, tu vas pas grandir sinon.`,
      },
    ],
  },
  {
    id: "2015-B-2",
    text: `(Scène : Au collège, le frère et un ami discutent. Le joueur les écoute.)
\\
\\Frère : Pourquoi les gars ont pas de vestiaire ? J'ai encore dû me changer dans le couloir...
\\Ami : Les filles en ont un, non ?
\\Frère : Ouais. Elles peuvent fermer la porte. Nous c'est genre "allez, vite, change-toi !"
\\Ami : Le prof dit qu'il y a pas la place pour un vestiaire garçons. Mais bon, c’est pas grave, non ?
\\Frère : Pas grave ? Tu veux que je me foute à poil ici ou quoi ?
\\Ami : Moi j’m’en fiche. T’es un peu trop sensible, non ?`,
    choices: [
      {
        text: "Qui regarde les gars, franchement ?",
        effect: { cynicism: 3 },
        followup: `\\Frère : Bah moi j’aime pas qu’on voie mon caleçon.
\\Ami : Les mecs, on s’en fiche ! L'été tout le monde enlève son t-shirt pour jouer au foot.
\\Frère : Moi je fais pas ça !
\\Ami : T’es vraiment un flippé.`,
      },
      {
        text: "T’as qu’à te changer aux toilettes !",
        effect: { individualism: 2 },
        followup: `\\Frère : Bah moi j’aime pas qu’on voie mon caleçon.
\\Ami : Les mecs, on s’en fiche ! L'été tout le monde enlève son t-shirt pour jouer au foot.
\\Frère : Moi je fais pas ça !
\\Ami : T’es vraiment un flippé.`,
      },
      {
        text: "Y a pas d’espace, faut faire avec.",
        effect: { authority: 2 },
        followup: `\\Frère : Bah moi j’aime pas qu’on voie mon caleçon.
\\Ami : Les mecs, on s’en fiche ! L'été tout le monde enlève son t-shirt pour jouer au foot.
\\Frère : Moi je fais pas ça !
\\Ami : T’es vraiment un flippé.`,
      },
      {
        text: "Se changer dans le couloir, c’est nul.",
        effect: { equality: 2 },
        followup: `\\Frère : Bah moi j’aime pas qu’on voie mon caleçon.
\\Ami : Les mecs, on s’en fiche ! L'été tout le monde enlève son t-shirt pour jouer au foot.
\\Frère : Moi je fais pas ça !
\\Ami : T’es vraiment un flippé.`,
      },
    ],
  },
  {
    id: "2015-B-3",
    text: `(Scène : à la maison, un après-midi du week-end)
\\
\\Frère : Quoi ? Pourquoi elle a 10 balles de plus ?
\\Soeur : J'ai besoin de maquillage. Toutes les filles en mettent. Si je fais pas pareil, je me sens à part.
\\Maman : Les filles ont plus de dépenses, c’est comme ça.
\\(Le joueur écoute en silence)`,
    choices: [
      {
        text: "C’est injuste !",
        effect: { authority: 2 },
        followup: `\\Joueur·se : Moi aussi j’ai des choses à acheter !
\\Frère : Tu vois ? J’suis pas le seul !
\\Soeur : (garde le silence)`,
      },
      {
        text: "Je veux aussi 10 balles de plus !",
        effect: { authority: 1 },
        followup: `\\Joueur·se : Alors je veux aussi plus.
\\Maman : (rit) Et tu veux acheter quoi, toi ?
\\Frère : (soupire)`,
      },
      {
        text: "Moi j’utilise pas de maquillage.",
        effect: { collective: 2, cynicism: 2 },
        followup: `\\Soeur : C’est vrai. Toi t’as pas besoin d’en acheter.
\\Frère : (air boudeur)`,
      },
    ],
  },
  {
    id: "2015-C-1",
    text: `(Scène : le soir, salon calme. Toute la famille regarde son téléphone.)
\\
\\Soeur : T’as vu ce truc ?
\\Frère : Quoi donc ?
\\Soeur : Une actrice peu connue a accusé un acteur célèbre de harcèlement. Il l’a nié et a dit qu’elle voulait juste se faire remarquer. Les commentaires sont en feu.
\\Frère : Il a pourtant donné à une fondation pour les femmes, non ? Maintenant, qui croire ?
\\(Le joueur regarde les commentaires. Certains sont violents, d’autres sceptiques.)`,
    choices: [
      {
        text: "Je veux croire la victime.",
        effect: { equality: 3, collective: 2 },
        followup: `\\Joueur·se : Si elle est allée jusqu'à porter plainte, c’est qu’il y a une raison.
\\Soeur : C’est ce que je pense aussi.
\\Frère : On peut pas en être sûr.`,
      },
      {
        text: "Attendons les résultats de l’enquête.",
        effect: { authority: 2 },
        followup: `\\Joueur·se : C’est à la justice de décider.`,
      },
      {
        text: "Je sais pas quoi penser...",
        effect: { cynicism: 2, individualism: 2 },
        followup: `\\Joueur·se : Je suis paumé·e. On n’en sait rien.`,
      },
      {
        text: "Y a peut-être une raison, mais laquelle ?",
        effect: { cynicism: 1, individualism: 2 },
        followup: `\\Soeur : (silence) Tu parles de qui, au juste ?
\\Frère : (ricane) Faut savoir lire entre les lignes.`,
      },
    ],
  },
  {
    id: "2015-C-2",
    text: `(Scène : le soir. Soeur rit devant son ordinateur.)
\\
\\Joueur·se : Tu regardes quoi ?
\\Frère : Encore un post qui dézingue les mecs ?
\\Soeur : Y avait un vieux qui critiquait sa salariée, il s’est fait griller. Et dans les commentaires : "typique d’un Han-nam", hahaha !
\\Frère : Donc maintenant, être un mec, c’est un crime ?
\\(Le joueur regarde. Des posts comme "Han-nam.txt" ou "Han-nam’s behavior" s’enchaînent.)`,
    choices: [
      {
        text: "C’est quand même abusé.",
        effect: { authority: 2 },
        followup: `\\Joueur·se : Y a des limites. Ces mots font mal.
\\Frère : (acquiesce)
\\Soeur : Bah fallait pas mal se comporter.`,
      },
      {
        text: "C’est drôle, faut l’avouer.",
        effect: { individualism: 2, cynicism: 1 },
        followup: `\\Joueur·se : Franchement, j’ai ri.
\\Frère : (soupire)
\\Soeur : (rit aussi)`,
      },
      {
        text: "Tous les commentaires sont comme ça ?", 
        effect: { collective: 4 },
        followup: `\\Joueur·se : C’est flippant, en vrai…
\\Soeur : T’as pas à flipper ! C’est juste drôle !`,
      },
      {
        text: "Ils le méritent bien.",
        effect: { cynicism: 2, equality: 2 },
        followup: `\\Joueur·se : Y a pas de fumée sans feu, non ?
\\Frère : (le regarde avec dégoûts)
\\Soeur : (amusée)`,
      },
    ],
  }
];