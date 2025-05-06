[System.Serializable]
public class KeyValue {
    public string key;
    public string value;
}

[System.Serializable]
public class SerializableDictionary {
    public List<KeyValue> items;

    public Dictionary<string, string> ToDictionary() {
        Dictionary<string, string> dict = new Dictionary<string, string>();
        foreach (var kv in items) {
            dict[kv.key] = kv.value;
        }
        return dict;
    }
}
