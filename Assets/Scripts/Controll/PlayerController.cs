using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Comtrol
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Mover _mover;
        [SerializeField] private Fighter _fighter;

        private Health _health;
        private Camera _mainCamera;
        private Ray GetMouseRay() => _mainCamera.ScreenPointToRay(Input.mousePosition);

        void Start()
        {
            _health = GetComponent<Health>();
            _mainCamera = Camera.main;
        }
        void Update()
        {
            if (_health.IsDead) { return; }

            if (InteractWithCombat()) { return; }

            if (InteractWithMovement()) { return; }


            Debug.Log("Nothing to do");
        }
        private bool InteractWithCombat()
        {
            var hits = Physics.RaycastAll(GetMouseRay());
            foreach (var hit in hits)
            {
                var target = hit.transform.GetComponent<CombatTarget>();
                if (target != null && _fighter.CanAttack(target.gameObject))
                {
                    if (Input.GetMouseButton(1))
                    {
                        _fighter.Attack(target.gameObject);
                        Debug.Log("Attack");
                    }
                    return true;
                }
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            if (Physics.Raycast(GetMouseRay(), out hit))
            {
                if (Input.GetMouseButton(1))
                {
                    _mover.StartMoveAction(hit.point);
                }
                return true;
            }
            return false;
        }


    }
}