using Zenject;
using Core.Player;
using UnityEngine;
using Core.Models;
using Core.Infrastructure.Signals.Game;
using Core.Infrastructure.Signals.UI;
using Unity.VisualScripting;

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

            _signalBus.Subscribe<CheckPossibilityOfBattle>(OnCheckBattle);
            _signalBus.Subscribe<Quest—ompleted>(_playerController.OnQuest—ompleted);

            return _playerController;
        }

        private void OnCheckBattle(CheckPossibilityOfBattle signal)
        {
            var interactable = _playerController.CheckBattle(signal.npc);
            _signalBus.Fire(new InteractableBattleButton { Value = interactable });

        }

        public void Tick()
        {
            _playerController?.Update();
        }

        void OnDestroy()
        {
            _signalBus.Unsubscribe<CheckPossibilityOfBattle>(OnCheckBattle);
            _signalBus.Unsubscribe<Quest—ompleted>(_playerController.OnQuest—ompleted);
        }
    }
}