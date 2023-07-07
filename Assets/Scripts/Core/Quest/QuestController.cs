using Core.Infrastructure.Signals.Game;
using Core.NPC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Core.Quest
{
    public class QuestController
    {
        private SignalBus _signalBus;
        private readonly QuestModel _model;
        private readonly QuestView _view;

        public QuestController(SignalBus signalBus, QuestModel model, QuestView view)
        {
            _signalBus = signalBus;
            _model = model;
            _view = view;
        }
    }
}