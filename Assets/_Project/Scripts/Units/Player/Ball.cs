using UnityEngine;

namespace _Project.Units.Player
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private float _damage = 1;
        
        public float Damage => _damage;

        public void SetDamage(float value)
        {
            _damage = value;
        }
    }
}