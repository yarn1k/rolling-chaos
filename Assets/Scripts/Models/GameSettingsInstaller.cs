using System;
using UnityEngine;
using Zenject;

namespace Core.Models
{
    [Serializable]
    public class GameSettings
    {
        public Vector3 OffsetCamera;
        [Range(1f, 10f)]
        public float SmoothSpeedCamera;
    }

    [Serializable]
    public class PlayerSettings
    {
        [Range(1f, 10f)]
        public float MovementSpeed;
    }

    [CreateAssetMenu(menuName = "Configuration/Settings/Game Settings")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [SerializeField]
        private GameSettings _gameSettings;
        [SerializeField]
        private PlayerSettings _playerSettings;

        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            //Container.Bind<ILogger>().To<StandaloneLogger>().AsCached();

            Container.Bind<GameSettings>().FromInstance(_gameSettings).IfNotBound();
            Container.Bind<PlayerSettings>().FromInstance(_playerSettings).IfNotBound();
        }
    }
}
