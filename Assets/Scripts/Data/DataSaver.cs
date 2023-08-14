using UnityEngine;

namespace Data
{
    public static class DataSaver
    {
        public static void Save<T>(T value)
        {
            PlayerPrefs.SetString("data", JsonUtility.ToJson(value, true));
        }

        public static T Load<T>()
        {
            var defaultValue = JsonUtility.ToJson(default(T), true);
            var json = PlayerPrefs.GetString("data", defaultValue);
            return JsonUtility.FromJson<T>(json);
        }
    }
}