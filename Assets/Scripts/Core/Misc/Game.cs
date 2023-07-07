using Core.Infrastructure.Signals.Game;
using Core.Player;
using Core.Quest;
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
        private QuestModel _initialQuest;

        public Game(SignalBus signalBus, 
                    PlayerFactory playerFactory,
                    CameraView cameraView, 
                    QuestModel initialQuest)
        {
            _signalBus = signalBus;
            _playerFactory = playerFactory;
            _cameraView = cameraView;
            _initialQuest = initialQuest;
        }

        void IInitializable.Initialize()
        {
            SpawnPlayer();
            InitCamera();
            InitQuest();
        }

        private void SpawnPlayer()
        {
            _playerController = _playerFactory.Create();
            //_playerController.Disable();
        }

        private void InitCamera()
        {
            _cameraView.InitTarget(_playerController.PlayerTransform);
        }

        private void InitQuest()
        {
            _playerController.InitQuest(_initialQuest);
        }
    }
}
