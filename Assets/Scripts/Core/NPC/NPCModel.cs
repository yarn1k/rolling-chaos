using Core.Proof;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.NPC
{
    public enum Gender
    {
        female = 0,
        male = 1
    }

    [Serializable]
    public class Relationships
    {
        public NPCModel npc;
        public string text;
    }

    [CreateAssetMenu(menuName = "Configuration/Models/Create NPC")]
    public class NPCModel : ScriptableObject
    {
        [Header("Base Parameters")]
        public string Name;
        [Range(18f, 100f)]
        public byte Age;
        public Gender Gender;
        public string AppearanceDescription;
        public string ShortBiography;
        public string PersonalityTraits;
        public string Goals;
        [Space]
        [Header("Game Parameters")]
        public Sprite Portrait;
        [Range(0f, 4f)]
        public byte Persuasion;
        [Range(0f, 4f)]
        public byte Intimidation;
        [Range(0f, 4f)]
        public byte Deception;
        [Range(0f, 4f)]
        public byte Insight;
        public List<ProofModel> Proofs;
        public List<Relationships> Relationships;
        public List<Relationships> Facts;
    }
}
