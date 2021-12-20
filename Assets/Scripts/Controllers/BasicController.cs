using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CarGame2D 
{
    public class BasicController : IDisposable
    {
        private List<BasicController> _basicControllers = new List<BasicController>();
        private List<GameObject> _gameObjects = new List<GameObject>();

        private bool _isDisposed;

        protected void AddController(BasicController basicController)
        {
            _basicControllers.Add(basicController);
        }

        protected void AddGameObject(GameObject gameObject)
        {
            _gameObjects.Add(gameObject);
        }

        public void Dispose()
        {
            if (_isDisposed)
                return;
            _isDisposed = true;

            foreach (var basicController in _basicControllers)
                basicController?.Dispose();

            _basicControllers.Clear();

            foreach (var cashedGameObject in _gameObjects)
                Object.Destroy(cashedGameObject);

            _gameObjects.Clear();

            OnDispose();
        }

        protected virtual void OnDispose()
        { }
    }
}

