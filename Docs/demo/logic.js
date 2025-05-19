let currentSceneIndex = 0;
let currentLang = "fr";
let scenes = scenes_fr; // scenes_kr/scenes_fr는 scenes.js에 정의돼 있어야 함

const traits = {
  authority: 1,
  individualism: 1,
  equality: 1,
  collective: 1,
  cynicism: 1,
};

let radar;

function setLang(lang) {
  currentLang = lang;
  scenes = lang === "kr" ? scenes_kr : scenes_fr;
  currentSceneIndex = 0;
}

function updateXYGraph(traits) {
  const canvas = document.getElementById("quadrant");
  const ctx = canvas.getContext("2d");
  const midX = canvas.width / 2;
  const midY = canvas.height / 2;
  const scale = 10;

  const x = traits.individualism - traits.collective; // 수정됨
  const y = traits.equality - traits.authority;
  const pointX = midX + x * scale;
  const pointY = midY - y * scale;

  ctx.clearRect(0, 0, canvas.width, canvas.height);

  ctx.beginPath();
  ctx.moveTo(midX, 0);
  ctx.lineTo(midX, canvas.height);
  ctx.moveTo(0, midY);
  ctx.lineTo(canvas.width, midY);
  ctx.strokeStyle = "#999";
  ctx.stroke();

  ctx.beginPath();
  ctx.arc(pointX, pointY, 6, 0, 2 * Math.PI);
  ctx.fillStyle = "red";
  ctx.fill();

  document.getElementById(
    "info"
  ).innerText = `x: ${x} (Indiv - Coll), y: ${y} (Eq - Auth)\n→ 위치: (${pointX.toFixed(
    0
  )}, ${pointY.toFixed(0)})`;
}

function updateStats() {
  document.getElementById("stats").textContent = JSON.stringify(
    traits,
    null,
    2
  );
  if (radar) {
    radar.data.datasets[0].data = [
      traits.authority,
      traits.individualism,
      traits.equality,
      traits.collective,
      traits.cynicism,
    ];
    radar.update();
  }
  updateXYGraph(traits); // 선택지 클릭 후 XY 좌표도 업데이트
}

function renderScene() {
  const scene = scenes[currentSceneIndex];
  document.getElementById("text").innerHTML = scene.text.replace(
    /\\/g,
    "<br/>"
  );

  const container = document.getElementById("choices");
  container.innerHTML = "";

  const followupBox = document.createElement("div");
  followupBox.id = "followupBox";
  followupBox.style.marginTop = "20px";
  container.appendChild(followupBox);

  const nextButton = document.createElement("button");
  nextButton.textContent = "다음";
  nextButton.style.display = "none";

  nextButton.onclick = () => {
    if (currentSceneIndex < scenes.length - 1) {
      currentSceneIndex++;
      renderScene();
    } else {
      document.getElementById("text").textContent = "게임이 끝났습니다.";
      container.innerHTML = "";
    }
  };

  scene.choices.forEach((choice) => {
    const btn = document.createElement("button");
    btn.textContent = choice.text;
    btn.onclick = () => {
      scene.choices.forEach((c) => c.button && (c.button.disabled = true));
      Object.entries(choice.effect).forEach(([k, v]) => (traits[k] += v));
      updateStats();
      followupBox.innerHTML = choice.followup.replace(/\\/g, "<br/>");
      nextButton.style.display = "block";
    };
    choice.button = btn;
    container.appendChild(btn);
  });

  container.appendChild(nextButton);
}

document.addEventListener("DOMContentLoaded", () => {
  const ctx = document.getElementById("radarChart").getContext("2d");
  radar = new Chart(ctx, {
    type: "radar",
    data: {
      labels: [
        "authority",
        "individualism",
        "equality",
        "collective",
        "cynicism",
      ],
      datasets: [
        {
          label: "My Traits",
          data: [
            traits.authority,
            traits.individualism,
            traits.equality,
            traits.collective,
            traits.cynicism,
          ],
          backgroundColor: "rgba(255, 99, 132, 0.2)",
          borderColor: "rgba(255, 99, 132, 1)",
          pointBackgroundColor: "rgba(255, 99, 132, 1)",
        },
      ],
    },
    options: {
      scales: {
        r: {
          min: 0,
          max: 16,
          ticks: { stepSize: 2 },
          pointLabels: {
            font: { size: 14 },
          },
        },
      },
    },
  });

  setLang("fr"); // 초기 언어 설정
  updateStats();
  renderScene();
});
