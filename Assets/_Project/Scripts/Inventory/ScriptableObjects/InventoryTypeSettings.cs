using _Project.Inventory.Interfaces;
using KiUtilities.Attributes;
using UnityEngine;

namespace _Project.Inventory.ScriptableObjects
{
    [CreateAssetMenu(fileName = "new Inventory Type", menuName = "Inventory/InventoryType", order = 0)]
    public class InventoryTypeSettings : ScriptableObject
    {
        public string Tittle;
        public Vector2Int Size;

        [RequireInterface(typeof(ICell)), SerializeField]
        private GameObject _cellTemplate;

        [RequireInterface(typeof(IItem)), SerializeField]
        private GameObject _itemTemplate;

        public ICell CellTemplate => _cellTemplate.GetComponent<ICell>();
        public IItem ItemTemplate => _itemTemplate.GetComponent<IItem>();
    }
}