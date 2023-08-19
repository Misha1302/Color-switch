namespace GameDataBase
{
    using System;
    using UnityEngine;

    // Need to call event from another task or thread
    public sealed class REvent : MonoBehaviour
    {
        private Action _actions;
        private bool _needToInvoke;

        private void Update()
        {
            TryToInvoke();
        }

        private void FixedUpdate()
        {
            TryToInvoke();
        }

        private void TryToInvoke()
        {
            if (!_needToInvoke) return;

            _actions.Invoke();
            _needToInvoke = false;
        }

        public void Subscribe(Action action)
        {
            _actions += action;
        }

        public void UnSubscribe(Action action)
        {
            _actions -= action;
        }

        public void Invoke()
        {
            _needToInvoke = true;
        }
    }
}