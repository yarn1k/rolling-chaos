using Core.Battle;
using Core.Infrastructure.Signals.Battle;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.Infrastructure.Installers
{
    public class BattleInstaller : MonoInstaller
    {
        [SerializeField]
        private Button _fight;

        public override void InstallBindings()
        {
            DeclareSignals();

            Container.Bind<Button>().FromInstance(_fight).AsSingle();
            Container.BindInterfacesTo<DialogueBattleSystem>().AsSingle();
            Container.Bind<AsyncProcessor>().FromNewComponentOnNewGameObject().AsSingle();
        }

        private void DeclareSignals()
        {
            Container.DeclareSignal<BattleSkillSelected>();
            Container.DeclareSignal<BattleProofSelected>();
            Container.DeclareSignal<BattleLogMessage>();
            Container.DeclareSignal<BattleProofUsed>();
        }
    }
}
