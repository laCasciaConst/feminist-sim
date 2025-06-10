using System.Collections.Generic;

public static class TraitCalculator
{
    public static void ApplyEffects(Dictionary<string, int> traits, List<ChoiceEffect> effects)
    {
        foreach (var effect in effects)
        {
            if (!traits.ContainsKey(effect.trait))
                traits[effect.trait] = 0;

            if (effect.mode == "add")
            {
                traits[effect.trait] += effect.value;
            }
            else if (effect.mode == "multiply")
            {
                traits[effect.trait] = (int)(traits[effect.trait] * effect.value);
            }
        }
    }

    public static void ApplyMultipliers(Dictionary<string, int> traits, string sceneId)
    {
        // 예시: 특정 sceneId 패턴에 따라 배수 적용
        if (sceneId.StartsWith("2016-B-Q"))
        {
            traits["cynicism"] = (int)(traits["cynicism"] * 1.5f);
        }
        else if (sceneId.StartsWith("2017-A-Q"))
        {
            traits["authority"] = (int)(traits["authority"] * 1.2f);
        }
    }
}
