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

        public void FaceTarget()
        {
            Vector3 direction = (_agent.destination - transform.position).normalized;
            Vector3 newDirection = new Vector3(direction.x, 0, direction.z);
            if (newDirection != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(newDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 2f);
            }
        }
    }
}
