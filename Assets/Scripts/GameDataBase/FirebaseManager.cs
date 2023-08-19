namespace GameDataBase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Firebase;
    using Firebase.Database;
    using Firebase.Extensions;
    using Newtonsoft.Json;
    using UnityEngine;

    public sealed class FirebaseManager : MonoBehaviour
    {
        public REvent initEvent;
        private FirebaseApp _app;
        private DatabaseReference _db;

        private void Awake()
        {
            initEvent = gameObject.AddComponent<REvent>();

            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                var dependencyStatus = task.Result;

                if (dependencyStatus == DependencyStatus.Available)
                    _app = FirebaseApp.DefaultInstance;
                else
                    throw new Exception($"Could not resolve all Firebase dependencies: {dependencyStatus}");

                _app.Options.DatabaseUrl = new Uri(FirebaseManagerSecrets.RealtimeDatabaseUrl);
                _db = FirebaseDatabase.GetInstance(FirebaseManagerSecrets.RealtimeDatabaseUrl).RootReference;

                initEvent.Invoke();
            });
        }

        public void GetAllUsers<T>(Action<List<T>> whenUsersReceived)
        {
            _db.Child("users").GetValueAsync().ContinueWithOnMainThread(t =>
            {
                if (t.IsFaulted || !t.IsCompleted)
                    throw t.Exception!;

                var enumerable = ((Dictionary<string, object>)t.Result.Value).Select(x => x.Value).Select(x =>
                {
                    var json = JsonConvert.SerializeObject((Dictionary<string, object>)x);
                    var obj = JsonUtility.FromJson<T>(json);
                    return obj;
                }).ToList();

                whenUsersReceived.Invoke(enumerable);
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