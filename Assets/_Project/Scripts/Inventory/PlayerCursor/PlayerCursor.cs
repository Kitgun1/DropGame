using System;
using _Project.Inventory.Interfaces;
using _Project.Inventory.Structures;
using KiUtilities.Attributes;
using UnityEngine;
using Zenject;

namespace _Project.Inventory.PlayerCursor
{
    public class PlayerCursor : MonoBehaviour
    {
        [RequireInterface(typeof(IItem)), SerializeField]
        private GameObject _itemTemplate;

        [SerializeField] private Canvas _canvas;

        [Inject] private PlayerInput _input;
        private IItem _cursorItem;
        private ItemSOProperty? _itemData;

        private void Start()
        {
            GameObject cursorItem = Instantiate(_itemTemplate, transform);
            _cursorItem = cursorItem.GetComponent<IItem>();
            _cursorItem.InitializeDisplay("", "", 0, null);

            InitializeListener();
        }

        private void InitializeListener()
        {
            _input.Mouse.Position.performed += ctx =>
            {
                _cursorItem.RectTransform.anchoredPosition = ctx.ReadValue<Vector2>() / _canvas.scaleFactor;
            };
        }

        #region CellClickActions

        public void OnClickDownLeft(ICell cell, IItem item, ItemSOProperty? cellItemData)
        {
            if (_itemData == null && cellItemData != null)
            {
                ReplaceData replaceData = cell.Take();
                _itemData = replaceData.ItemData;
                _cursorItem.InitializeDisplay(_itemData.Value.Tittle,
                    _itemData.Value.Description,
                    replaceData.Amount,
                    _itemData.Value.Icon);
            }
            else if (_itemData != null && cellItemData == null)
            {
                cell.Put(_cursorItem.Amount, _itemData);
                _itemData = null;
                _cursorItem.InitializeDisplay("", "", 0, null);
            }
            else if (_itemData != null && cellItemData != null && _itemData == cellItemData)
            {
                uint put = cell.Put(_cursorItem.Amount);
                _cursorItem.Amount -= put;
                if (_cursorItem.Amount == 0) _itemData = null;
            }
            else if (_itemData != null && cellItemData != null && _itemData != cellItemData)
            {
                Replace(cell);
            }
        }

        public void OnClickUpLeft(ICell cell, IItem item, ItemSOProperty? cellItemData)
        {
        }

        public void OnClickDownRight(ICell cell, IItem item, ItemSOProperty? cellItemData)
        {
            if (_itemData == null && cellItemData != null)
            {
                uint take = item.Amount / 2;
                if (item.Amount % 2 != 0) take++;

                ReplaceData replaceData = cell.Take(take);
                _itemData = replaceData.ItemData;
                _cursorItem.InitializeDisplay(_itemData.Value.Tittle,
                    _itemData.Value.Description,
                    replaceData.Amount,
                    _itemData.Value.Icon);
            }
            else if (_itemData != null && cellItemData == null)
            {
                cell.Put(1, _itemData);
                _cursorItem.Amount -= 1;
                if (_cursorItem.Amount == 0) _itemData = null;
            }
            else if (_itemData != null && cellItemData != null && _itemData == cellItemData)
            {
                if (_cursorItem.Amount < _itemData.Value.MaxAmount)
                {
                    cell.Take(1);
                    _cursorItem.Amount += 1;
                }
            }
            else if (_itemData != null && cellItemData != null && _itemData != cellItemData)
            {
                Replace(cell);
            }
        }

        public void OnClickUpRight(ICell cell, IItem item, ItemSOProperty? cellItemData)
        {
        }

        public void OnClickDownMiddle(ICell cell, IItem item, ItemSOProperty? cellItemData)
        {
        }

        public void OnClickUpMiddle(ICell cell, IItem item, ItemSOProperty? cellItemData)
        {
        }

        #endregion

        public bool TryThrowItem(uint amount)
        {
            if (_itemData == null) return false;

            if (amount > _cursorItem.Amount) _cursorItem.Amount = 0;
            else _cursorItem.Amount -= amount;
            if (_cursorItem.Amount == 0) _itemData = null;
            // TODO: throw item in world;
            return true;
        }

        private void Replace(ICell cell)
        {
            if (_itemData == null) throw new NullReferenceException();
            ReplaceData cellReplaceData = cell
                .Replace(new ReplaceData((ItemSOProperty)_itemData, _cursorItem.Amount));
            _cursorItem.InitializeDisplay(cellReplaceData.ItemData.Tittle,
                cellReplaceData.ItemData.Description,
                cellReplaceData.Amount,
                cellReplaceData.ItemData.Icon);
            _itemData = cellReplaceData.ItemData;
        }
    }
}