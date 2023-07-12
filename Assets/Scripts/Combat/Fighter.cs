using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] private float _weaponRange = 3f;
        [SerializeField] private float _timeBetweenAttacks = 1f;
        [SerializeField] private float _weaponDamage = 5f;

        private Animator _animator;
        private Mover _mover;

        private Health _target;
        private float _timeSinceLastAttack = 0;


       // private bool IsTargetInRange() => Vector3.Distance(transform.position, _target.transform.position) < _weaponRange;

        private void Start()
        {
            _mover = GetComponent<Mover>();
            _animator = GetComponent<Animator>();
        }
        void Update()
        {
            _timeSinceLastAttack += Time.deltaTime;
            if (_target == null) return;
            if (_target.IsDead) return;

            bool isTargetInRange = Vector3.Distance(transform.position, _target.transform.position) < _weaponRange;
            if (!isTargetInRange)
            {
                _mover.MoveTo(_target.transform.position);
            }
            else
            {
                _mover.Cansel();
                AttackBehaviour();
            }
        }
       
        public bool CanAttack(GameObject target)
        {
            var targetHealth = target.GetComponent<Health>();
            return targetHealth != null && !targetHealth.IsDead;
        }
        public void Attack(GameObject target)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            _target = target.GetComponent<Health>();
        }
        private void AttackBehaviour()
        {
            
            transform.LookAt(_target.transform);
            if (_timeSinceLastAttack >= _timeBetweenAttacks)
            {
                //Thiss will trigger the Hit() event
                TriggerAttackAnimation();
                _timeSinceLastAttack = 0;
            }
        }

        private void TriggerAttackAnimation()
        {
            _animator.ResetTrigger("stopAttack");
            _animator.SetTrigger("attack");
        }

        //Animation event
        private void Hit()
        {
            if (_target != null)
            {
                _target.TakeDamage(_weaponDamage);
            }
        }

        public void Cansel()
        {
            _animator.ResetTrigger("attack");
            _animator.SetTrigger("stopAttack");
            _target = null;
        }
    }
}

