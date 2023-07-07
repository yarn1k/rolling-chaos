using Core.NPC;
using Core.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.NPC
{
    public class NPCController
    {
        private readonly NPCModel _model;
        private readonly NPCView _view;

        public NPCController(NPCModel model, NPCView view)
        {
            _model = model;
            _view = view;
        }
    }
}
