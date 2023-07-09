using UnityEngine;

namespace Core.Player
{
    public class PlayerModel
    {
        public string Name;
        public Sprite Portrait;
        public float MovementSpeed;
        [Range(0f, 4f)]
        public byte Persuasion;
        [Range(0f, 4f)]
        public byte Intimidation;
        [Range(0f, 4f)]
        public byte Deception;
        [Range(0f, 4f)]
        public byte Insight;
        
        public PlayerModel(
            string name,
            Sprite portrait,
            float movementSpeed,
            byte persuasion, 
            byte intimidation, 
            byte deception, 
            byte insight
        )
        {
            Name = name;
            Portrait = portrait;
            MovementSpeed = movementSpeed;
            Persuasion = persuasion;
            Intimidation = intimidation;
            Deception = deception;
            Insight = insight;
        }
    }
}
