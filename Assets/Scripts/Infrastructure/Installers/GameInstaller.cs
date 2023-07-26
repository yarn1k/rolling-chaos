using Core.Infrastructure.Signals.Game;
using Core.Player;
using Core.Quest;
using Core.UI;
using UnityEngine;
using Zenject;

namespace Core.Infrastructure.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField]
        private CameraView _cameraView;
        [SerializeField]
        private QuestModel _initialQuest;

        public override void InstallBindings()
        {
            DeclareSignals();

            Container.Bind<CameraView>().FromInstance(_cameraView).AsSingle();
            Container.Bind<QuestModel>().FromInstance(_initialQuest).AsSingle();
            Container.BindInterfacesTo<Game>().AsSingle();

            BindFactories();
        }

        private void DeclareSignals()
        {
            Container.DeclareSignal<CheckPossibilityOfBattle>();
            Container.DeclareSignal<BattleLoadScene>();
            //Container.DeclareSignal<QuestÑompleted>();
        }

        private void BindFactories()
        {
            Container.BindInterfacesAndSelfTo<PlayerFactory>().AsSingle();
        }
    }
}