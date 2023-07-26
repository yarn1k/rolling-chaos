using Core.Player;
using Core.Quest;
using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace Core.UI
{
    public class QuestPanelView : MonoBehaviour, IDisposable
    {
        private SignalBus _signalBus;
        private TMP_Text _questName;

        [Inject]
        private void Construct(SignalBus signalBus, QuestModel initialQuest)
        {
            _signalBus = signalBus;
        }

        private void Awake()
        {
            _questName = GetComponent<TMP_Text>();
        }

        private void Start()
        {
            _questName.text = PlayerController.CurrentQuest.Name;
        }

        public void Dispose()
        {
        }
    }
}