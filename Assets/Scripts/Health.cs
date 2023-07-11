using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float _healtPoints = 100f;

        private bool _isDead = false;
        public bool IsDead
        {
            get { return _isDead; }
        }

        public void TakeDamage(float damage)
        {
            if (_isDead) return;

            _healtPoints = Mathf.Max(_healtPoints - damage, 0);

            if (_healtPoints == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            GetComponent<Animator>().SetTrigger("die");
            _isDead = true;
        }
    }
}
