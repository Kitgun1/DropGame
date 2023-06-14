using _Project.Inventory.Structures;
using UnityEngine;

namespace _Project.Inventory.ScriptableObjects
{
    [CreateAssetMenu(fileName = "new Item", menuName = "Inventory/Item", order = 0)]
    public class ItemSO : ScriptableObject
    {
        public ItemSOProperty Data;
    }
}