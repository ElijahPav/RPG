using RPG.Combat;
using UnityEngine;

namespace RPG.Controll
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float _chaseDistance = 5f;

        private Fighter _fighter;
        private string _playerTag = "Player";
        private GameObject _player;

        private void Start()
        {
            _fighter = GetComponent<Fighter>();
            _player = GameObject.FindGameObjectWithTag(_playerTag);

        }
        private void Update()
        {
            if (_player == null) { return; }

            if (IsPlayerInAttackRange() && _fighter.CanAttack(_player))
            {
                _fighter.Attack(_player);
            }
            else
            {
                _fighter.Cansel();
            }

        }

        private bool IsPlayerInAttackRange()
        {
            var distanceTopLayer = Vector3.SqrMagnitude(transform.position - _player.transform.position);
            Debug.Log(distanceTopLayer);
            return distanceTopLayer < (_chaseDistance * _chaseDistance);
        }
    }
}

