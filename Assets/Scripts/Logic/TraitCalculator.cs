public enum TraitMode { Add, Multiply }

[System.Serializable]
public class TraitEffect
{
    public string trait;
    public float value;
    public TraitMode mode;
}

public static class TraitCalculator
{
    public static Dictionary<string, float> ApplyTraits(
        List<TraitEffect> effects,
        Dictionary<string, float> current,
        string sceneId
    )
    {
        Dictionary<string, float> result = new Dictionary<string, float>(current);

        foreach (var effect in effects)
        {
            string trait = effect.trait;
            float value = effect.value;
            TraitMode mode = effect.mode;

            if (!result.ContainsKey(trait))
                result[trait] = 0;

            float multiplier = GetSceneMultiplier(sceneId, trait);

            if (mode == TraitMode.Add)
                result[trait] += value;
            else if (mode == TraitMode.Multiply)
                result[trait] *= value;

            // 특수 multiplier 적용
            result[trait] *= multiplier;
        }

        return result;
    }

    private static float GetSceneMultiplier(string sceneId, string trait)
    {
        if (sceneId.StartsWith("2016-B-Q") || sceneId.StartsWith("2017-A-Q"))
        {
            if (trait == "authority") return 4f;
            if (trait == "collective") return 3f;
        }
        return 1f;
    }
}

