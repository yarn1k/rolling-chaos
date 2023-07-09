using Core.Infrastructure.Signals.Game;
using Core.Infrastructure.Signals.UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.UI
{
    public class DialogueView : Panel
    {
        private SignalBus _signalBus;
        [SerializeField]
        private Image _portrait;
        [SerializeField]
        private Button _startBattle;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;

            _signalBus.Subscribe<OpenDialoguePanel>(InitPanel);
            _signalBus.Subscribe<CloseDialoguePanel>(Hide);
            _signalBus.Subscribe<InteractableBattleButton>(SetInteractableButton);

            _startBattle.onClick.AddListener(StartBattle);
        }

        private void InitPanel(OpenDialoguePanel signal)
        {
            SetPortrait(signal.Portrait);
            Show();
        }

        private void SetInteractableButton(InteractableBattleButton signal)
        {
            _startBattle.interactable = signal.Value;
        }

        private void SetPortrait(Sprite portrait) 
        {
            _portrait.sprite = portrait;
        }

        public void StartBattle()
        {
            _signalBus.Fire(new BattleLoadScene {});
            //SceneManager.LoadScene(Constants.Scenes.Battle);
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<OpenDialoguePanel>(InitPanel);
            _signalBus.Unsubscribe<CloseDialoguePanel>(Hide);
            _signalBus.Unsubscribe<InteractableBattleButton>(SetInteractableButton);
        }
    }
}