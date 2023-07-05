using UnityEngine;
using UnityEngine.AI;

namespace Core.Player
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerView : MonoBehaviour
    {
        private NavMeshAgent _agent;
        public LayerMask Ground;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        public void InitAgent(float speed)
        {
            _agent.speed = speed;
        }

        public void Move(RaycastHit hit)
        {
            _agent.SetDestination(hit.point);
        }
    }
}
