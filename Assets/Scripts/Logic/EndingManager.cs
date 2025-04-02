using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EndingCondition {
    public string ending_id;
    public string title;
    public CoordinateConditions coordinate_conditions;
    public List<string> required_flags;
    public string description;
}

[System.Serializable]
public class CoordinateConditions {
    public string x;
    public string y;
}

public class EndingManager : MonoBehaviour {
    public List<EndingCondition> endingConditions;

    // 좌표와 플래그를 받아서 가장 먼저 만족하는 엔딩 리턴
    public string DetermineEnding(int x, int y, HashSet<string> flags) {
        foreach (var ec in endingConditions) {
            if (CheckCoordinate(ec.coordinate_conditions.x, x) &&
                CheckCoordinate(ec.coordinate_conditions.y, y) &&
                CheckFlags(ec.required_flags, flags)) {
                return ec.title;
            }
        }
        return "기본 엔딩";
    }

    private bool CheckCoordinate(string condition, int value) {
        if (condition.Contains("between")) {
            var parts = condition.Replace("between ", "").Split(" and ");
            int min = int.Parse(parts[0]);
            int max = int.Parse(parts[1]);
            return value >= min && value <= max;
        }
        if (condition.StartsWith(">=")) return value >= int.Parse(condition.Substring(2));
        if (condition.StartsWith("<=")) return value <= int.Parse(condition.Substring(2));
        return false;
    }

    private bool CheckFlags(List<string> requiredFlags, HashSet<string> playerFlags) {
        foreach (var flag in requiredFlags) {
            if (!playerFlags.Contains(flag)) return false;
        }
        return true;
    }

    void Start() {
        LoadEndingConditions();
    }

    void LoadEndingConditions() {
        TextAsset json = Resources.Load<TextAsset>("Configs/ending_conditions");
        endingConditions = JsonUtilityWrapper.FromJsonArray<EndingCondition>(json.text);
    }
}
