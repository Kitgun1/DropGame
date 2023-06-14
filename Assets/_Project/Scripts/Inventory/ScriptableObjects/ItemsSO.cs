using System.Collections.Generic;
using UnityEngine;

namespace _Project.Inventory.ScriptableObjects
{
    [CreateAssetMenu(fileName = "new Items Pool", menuName = "Inventory/Item Pool", order = 0)]
    public class ItemsSO : ScriptableObject
    {
        [SerializeField] private List<ItemSO> _items = new List<ItemSO>();

        public List<ItemSO> Items => _items;
    }
}