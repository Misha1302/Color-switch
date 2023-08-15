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

                const string uri = "https://color-switch-d7357-default-rtdb.firebaseio.com";
                _app.Options.DatabaseUrl = new Uri(uri);
                _db = FirebaseDatabase.GetInstance(uri).RootReference;
            });
        }

        public void SetUser<T>(T user) where T : IHaveId
        {
            var json = JsonUtility.ToJson(user);
            _db.Child("users").Child(user.GetId()).SetRawJsonValueAsync(json);
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
                    var json = (Dictionary<string, object>)task.Result.Value;
                    var obj = JsonUtility.FromJson<T>(JsonConvert.SerializeObject(json));
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