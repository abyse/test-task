using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using DigitalRubyShared;

namespace TestTask
{
    public class Application : MonoBehaviour
    {
        [SerializeField] private Cube _cube;

        [SerializeField] private Field _field;
        
        private InputHandler _inputHandler;

        private ApplicationSession _session;

        private bool _isApplicationQuit = false;
        
        #region MonoBehaviour
        
        private void Start()
        {
            ResolveDependencies();
            StartSession();
        }

        #endregion
        
        private void ResolveDependencies()
        {
            _session = new ApplicationSession(_cube, _field);
            _inputHandler = new InputHandler();
            _cube.Initialize(_inputHandler);
        }

        private async void StartSession()
        {
            while (!_isApplicationQuit)
            {
                await _session.RunSession();
                await UniTask.Yield();
            }
        }

        private void OnApplicationQuit()
        {
            _isApplicationQuit = true;
        }
    }
}
