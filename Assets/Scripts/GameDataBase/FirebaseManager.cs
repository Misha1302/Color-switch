namespace GameDataBase
{
    using System;
    using System.Collections.Generic;
    using Firebase;
    using Firebase.Database;
    using Firebase.Extensions;
    using Newtonsoft.Json;
    using UnityEngine;

    public sealed class FirebaseManager : MonoBehaviour
    {
        private FirebaseApp _app;
        private DatabaseReference _db;

        private void Awake()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                var dependencyStatus = task.Result;

                if (dependencyStatus == DependencyStatus.Available)
                    _app = FirebaseApp.DefaultInstance;
                else
                    throw new Exception($"Could not resolve all Firebase dependencies: {dependencyStatus}");

                _app.Options.DatabaseUrl = new Uri(FirebaseManagerSecrets.RealtimeDatabaseUrl);
                _db = FirebaseDatabase.GetInstance(FirebaseManagerSecrets.RealtimeDatabaseUrl).RootReference;
            });
        }

        public void SetUser<T>(T user) where T : IHaveId
        {
            var json = JsonUtility.ToJson(user);
            var userId = user.GetId();
            _db.Child("users").Child(userId).SetRawJsonValueAsync(json);
        }

        public void GetUser<T>(string userId, Action<T> whenUserReceived) where T : IHaveId
        {
            _db.Child("users").Child(userId).GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted || !task.IsCompleted)
                {
                    whenUserReceived.Invoke(default);
                    return;
                }

                try
                {
                    var dict = (Dictionary<string, object>)task.Result.Value;
                    var json = JsonConvert.SerializeObject(dict);
                    var obj = JsonUtility.FromJson<T>(json);
                    whenUserReceived.Invoke(obj);
                }
                catch
                {
                    whenUserReceived.Invoke(default);
                }
            });
        }
    }
}