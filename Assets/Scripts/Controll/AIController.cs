using RPG.Combat;
using RPG.Control;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Controll
{
    public class AIController : MonoBehaviour
    {
        private const string _playerTag = "Player";

        [SerializeField] private float _chaseDistance = 5f;
        [SerializeField] private float _suspicionTime = 5f;
        [SerializeField] private float _waypointDdwellTime = 4f;
        [SerializeField] private PatrolPath _patrolPath;
        [SerializeField] private float waypointToleranceDistance = 1f;

        private Fighter _fighter;
        private Mover _mover;
        private Health _health;

        private GameObject _player;
        private Vector3 _guardPosition;
        private float _timeSinceLastSawPlayer = Mathf.Infinity;
        private float _timeSinceArrivedAtWaypoint = Mathf.Infinity;
        private int currentWaypointIndex = 0;

        private void Start()
        {
            _fighter = GetComponent<Fighter>();
            _mover = GetComponent<Mover>();
            _health = GetComponent<Health>();
            _player = GameObject.FindGameObjectWithTag(_playerTag);

            _guardPosition = transform.position;
        }
        private void Update()
        {
            if (_player == null || _health.IsDead) { return; }

            if (IsPlayerInAttackRange() && _fighter.CanAttack(_player))
            {
                AttackBehaviour();
            }
            else if (_timeSinceLastSawPlayer < _suspicionTime)
            {
                SuspicionBehaviour();
            }
            else if (_timeSinceArrivedAtWaypoint >= _waypointDdwellTime)
            {
                PatrolBehaviour();
            }
            UpdateTimers();

        }

        private void UpdateTimers()
        {
            _timeSinceLastSawPlayer += Time.deltaTime;
            _timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            var nextPosition = _guardPosition;
            if (_patrolPath != null)
            {
                if (AtWaypoint())
                {
                    UpdateWaypointIndex();
                    _timeSinceArrivedAtWaypoint = 0;
                }
                nextPosition = GetCurrentWaypointPosition();
            }
            _mover.StartMoveAction(nextPosition);
        }

        private bool AtWaypoint()
        {
            return Vector3.SqrMagnitude(transform.position - GetCurrentWaypointPosition()) <= waypointToleranceDistance;
        }

        private void UpdateWaypointIndex()
        {
            currentWaypointIndex = _patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private Vector3 GetCurrentWaypointPosition()
        {
            return _patrolPath.GetWaypointPosition(currentWaypointIndex);
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrenAction();
        }

        private void AttackBehaviour()
        {
            _timeSinceLastSawPlayer = 0;
            _fighter.Attack(_player);
        }

        private bool IsPlayerInAttackRange()
        {
            var distanceTopLayer = Vector3.SqrMagnitude(transform.position - _player.transform.position);
            return distanceTopLayer < (_chaseDistance * _chaseDistance);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _chaseDistance);
        }
    }
}

