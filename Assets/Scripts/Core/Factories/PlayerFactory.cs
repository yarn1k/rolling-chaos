using Zenject;
using Core.Player;
using UnityEngine;
using Core.Models;

namespace Core
{
    public class PlayerFactory : ITickable, IFactory<PlayerController>
    {
        private readonly DiContainer _container;
        private readonly SignalBus _signalBus;
        private readonly PlayerSettings _playerSettings;
        private PlayerController _playerController;

        private const string PATH = "Prefabs/Player/Player";

        public PlayerFactory(DiContainer container, SignalBus signalBus, PlayerSettings playerSettings)
        {
            _container = container;
            _signalBus = signalBus;
            _playerSettings = playerSettings;
        }

        public PlayerController Create()
        {
            PlayerView asset = Resources.Load<PlayerView>(PATH);
            PlayerView view = GameObject.Instantiate<PlayerView>(asset);

            PlayerModel model = _container.Instantiate<PlayerModel>(new object[] { _playerSettings.MovementSpeed });
            _container.Bind<PlayerModel>().FromInstance(model).AsSingle();

            _playerController = new PlayerController(model, view);
            return _playerController;
        }

        public void Tick()
        {
            _playerController?.Update();
        }
    }
}