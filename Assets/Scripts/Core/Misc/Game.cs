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
        private CameraView _cameraView;

        public Game(SignalBus signalBus, PlayerFactory playerFactory, CameraView cameraView)
        {
            _signalBus = signalBus;
            _playerFactory = playerFactory;
            _cameraView = cameraView;
        }

        void IInitializable.Initialize()
        {
            SpawnPlayer();
            _cameraView.InitTarget(_playerController.PlayerTransform);
        }

        private void SpawnPlayer()
        {
            _playerController = _playerFactory.Create();
            //_playerController.Disable();
        }
    }
}
