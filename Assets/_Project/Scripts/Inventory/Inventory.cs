using _Project.Inventory.Interfaces;
using _Project.Inventory.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Inventory
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private InventoryTypeSettings _inventoryTypeSettings;

        [Inject] private PlayerCursor.PlayerCursor _playerCursor;
        private ICell[,] _cells;

        private void Start()
        {
            GridLayoutGroup grid = _parent.GetComponent<GridLayoutGroup>();
            grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            grid.constraintCount = _inventoryTypeSettings.Size.x;

            _cells = new ICell[_inventoryTypeSettings.Size.x, _inventoryTypeSettings.Size.y];
            for (int y = 0; y < _cells.GetLength(1); y++)
            {
                for (int x = 0; x < _cells.GetLength(0); x++)
                {
                    ICell cell = Instantiate(_inventoryTypeSettings.CellTemplate.Transform, _parent)
                        .GetComponent<ICell>();
                    cell.SetCursor(_playerCursor);
                    cell.Initialize(_inventoryTypeSettings.ItemTemplate);
                    _cells[x, y] = cell;
                }
            }
        }
    }
}