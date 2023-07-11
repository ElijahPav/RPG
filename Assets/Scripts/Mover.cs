using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        private const string forwardSpeedAnimatorName = "forwardSpeed";
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;

        private Camera _mainCamera;

        void Start()
        {
            _navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
            _animator = gameObject.GetComponent<Animator>();
            _mainCamera = Camera.main;
        }

        void Update()
        {
            UpdateAnimate();
        }

        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }
        public void MoveTo(Vector3 point)
        {
            _navMeshAgent.destination = point;
            _navMeshAgent.isStopped = false;
        }

        public void Cansel()
        {
            _navMeshAgent.isStopped = true;
        }
        private void UpdateAnimate()
        {
            var velocity = transform.InverseTransformDirection(_navMeshAgent.velocity);
            var forwardSpeed = velocity.z;

            _animator.SetFloat(forwardSpeedAnimatorName, forwardSpeed);

        }
    }
}