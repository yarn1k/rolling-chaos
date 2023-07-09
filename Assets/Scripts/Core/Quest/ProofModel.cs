using Core.Quest;
using UnityEngine;

namespace Core.Proof
{
    public enum Type
    {
        evidence = 0,
        fact = 1
    }

    public enum Combination
    {
        Persuasion = 0,
        Intimidation = 1,
        Deception = 2,
        Insight = 3
    }

    [CreateAssetMenu(menuName = "Configuration/Models/Create Proof")]
    public class ProofModel : ScriptableObject
    {
        public string Name;
        public Type Type;
        public QuestModel Quest;
        public Combination Combination;
    }
}
