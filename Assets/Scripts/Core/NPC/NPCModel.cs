using UnityEngine;

namespace Core.NPC
{
    public enum Gender
    {
        female = 0,
        male = 1
    }

    [CreateAssetMenu(menuName = "Configuration/Models/Create NPC")]
    public class NPCModel : ScriptableObject
    {
        public string Name;
        [Range(18f, 100f)]
        public byte Age;
        public Gender Gender;
        public Sprite Portrait;
        public string AppearanceDescription;
        public string ShortBiography;
        public string PersonalityTraits;
        public string Goals;
        [Range(0f, 4f)]
        public byte Persuasion;
        [Range(0f, 4f)]
        public byte Intimidation;
        [Range(0f, 4f)]
        public byte Deception;
        [Range(0f, 4f)]
        public byte Insight;
    }
}
