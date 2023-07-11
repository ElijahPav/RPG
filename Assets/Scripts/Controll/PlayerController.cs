using RPG.Combat;
using RPG.Movement;
using UnityEngine;

namespace RPG.Comtrol
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Mover _mover;
        [SerializeField] private Fighter _fighter;

        private Camera _mainCamera;
        private Ray GetMouseRay() => _mainCamera.ScreenPointToRay(Input.mousePosition);

        void Start()
        {
            _mainCamera = Camera.main;
        }
        void Update()
        {
            if (InteractWithCombat()) return;


            if (InteractWithMovement()) return;


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
                    if (Input.GetMouseButtonDown(1))
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
                    Debug.Log("Move");
                }
                return true;
            }
            return false;
        }


    }
}