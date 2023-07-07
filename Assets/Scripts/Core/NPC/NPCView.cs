using Core.Infrastructure.Signals.Game;
using Core.Infrastructure.Signals.UI;
using UnityEngine;
using Zenject;

namespace Core.NPC
{
    public class NPCView : MonoBehaviour
    {
        private SignalBus _signalBus;
        [SerializeField]
        private NPCModel _model;
        private bool _playerInRange = false;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void OnMouseDown()
        {
            if (_playerInRange)
            {
                _signalBus.Fire(new CheckPossibilityOfBattle { npc = _model });
                _signalBus.Fire(new OpenDialoguePanel { Portrait = _model.Portrait });
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _playerInRange = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _playerInRange = false;
                _signalBus.Fire(new CloseDialoguePanel {});
            }
        }
    }
}
