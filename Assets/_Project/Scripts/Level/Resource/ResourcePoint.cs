using _Project.Inventory.ScriptableObjects;
using _Project.Units.Player;
using JetBrains.Annotations;
using KiUtilities.CustomComponent;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Level.Resource
{
    public class ResourcePoint : MonoBehaviour
    {
        [SerializeField] private KiSlider _sliderDurability;
        [SerializeField] private Image _iconResource;

        private ItemSO _itemSO;
        private float _durability;

        public ResourcePoint Initialize([NotNull] ItemSO itemSO)
        {
            _itemSO = itemSO;
            _durability = _itemSO.Data.MaxDurability;
            UpdateVisual();
            return this;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.TryGetComponent(out Ball ball)) return;
            TakeDamage(ball.Damage);
            UpdateVisual();
        }

        private void TakeDamage(float damage)
        {
            _durability -= Mathf.Abs(damage);
        }

        private void UpdateVisual()
        {
            _sliderDurability.Value = _durability;
            _iconResource.sprite = _itemSO.Data.Icon;
        }
    }
}