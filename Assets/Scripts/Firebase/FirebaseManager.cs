namespace Firebase
{
    using System;
    using Firebase.Database;
    using Firebase.Extensions;
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


                _db = FirebaseDatabase.DefaultInstance.RootReference;
            });
        }

        public void SetUser<T>(T user) where T : IHaveId
        {
            var json = JsonUtility.ToJson(user);
            _db.Child("users").Child(user.Id).SetRawJsonValueAsync(json);
        }

        public void GetUser<T>(string userId, Action<T> whenUserReceived) where T : IHaveId
        {
            _db.Child("users").Child(userId).GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted) throw task.Exception!;

                if (!task.IsCompleted) return;

                var json = (string)task.Result.Value;
                whenUserReceived.Invoke(JsonUtility.FromJson<T>(json));
            });
        }
    }
}