using UnityEngine;

namespace Core.Player
{
    public class PlayerModel
    {
        public string Name;
        public Sprite Portrait;
        public float MovementSpeed;
        
        public PlayerModel(float movementSpeed)
        {
            MovementSpeed = movementSpeed;
        }
    }
}
