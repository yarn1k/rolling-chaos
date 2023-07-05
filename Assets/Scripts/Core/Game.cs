using Core.Player;
using Zenject;
using IInitializable = Zenject.IInitializable;

namespace Core
{
    public class Game : IInitializable
    {
        private readonly SignalBus _signalBus;
        private readonly PlayerFactory _playerFactory;
        private PlayerController _playerController;

        public Game(SignalBus signalBus, PlayerFactory playerFactory)
        {
            _signalBus = signalBus;
            _playerFactory = playerFactory;
        }

        void IInitializable.Initialize()
        {
            SpawnPlayer();
        }

        private void SpawnPlayer()
        {
            _playerController = _playerFactory.Create();
            //_playerController.Disable();
        }
    }
}
