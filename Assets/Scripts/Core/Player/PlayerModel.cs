using UnityEngine;

namespace Core.Player
{
    public class PlayerModel
    {
        public string Name;
        public Sprite Portrait;
        [Range(0f, 4f)]
        public byte Persuasion;
        [Range(0f, 4f)]
        public byte Intimidation;
        [Range(0f, 4f)]
        public byte Deception;
        [Range(0f, 4f)]
        public byte Insight;
        public float MovementSpeed;

        public PlayerModel(
            Sprite portrait, 
            byte persuasion, 
            byte intimidation, 
            byte deception, 
            byte insight, 
            float movementSpeed)
        {
            Portrait = portrait;
            Persuasion = persuasion;
            Intimidation = intimidation;
            Deception = deception;
            Insight = insight;
            MovementSpeed = movementSpeed;
        }
    }
}
