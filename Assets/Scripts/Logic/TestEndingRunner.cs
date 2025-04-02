
using System.Collections.Generic;
using UnityEngine;

public class TestEndingRunner : MonoBehaviour
{
    public EndingManager endingManager;

    void Start()
    {
        // 임시로 좌표 값 설정
        int x = 4;  // 질서감 (authority)
        int y = 4;  // 자기확신 (self-assurance)

        // 테스트용 플래그 예시 (사용자가 게임 중 획득한 상태를 가정)
        HashSet<string> flags = new HashSet<string> {
            "rejected_all_blame",  // 강철의 논리 진입 조건 충족
            "refused_every_request"
        };

        // 엔딩 결과 확인
        string result = endingManager.DetermineEnding(x, y, flags);
        Debug.Log("🎮 최종 엔딩: " + result);
    }
}
