using Core.NPC;
using Core.Proof;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Quest
{
    public enum QuestType
    {
        QuestGiver = 0,
        QuestHelper = 1,
        QuestFinisher = 2
    }

    [Serializable]
    public class NPC
    {
        public NPCModel Model;
        public QuestType QuestType;
    }

    [CreateAssetMenu(menuName = "Configuration/Models/Create Quest")]
    public class QuestModel : ScriptableObject
    {
        public string Name;
        public QuestModel NextQuest;
        public List<NPC> NPC;
    }
}