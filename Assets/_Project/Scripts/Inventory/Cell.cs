using System;
using System.Collections.Generic;
using _Project.Inventory.Interfaces;
using _Project.Inventory.ScriptableObjects;
using _Project.Inventory.Structures;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using Random = UnityEngine.Random;

namespace _Project.Inventory
{
    public class Cell : MonoBehaviour, ICell, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private List<ItemSO> _defs;
        [SerializeField] private Transform _parent;

        private ItemSOProperty? _itemData;
        private IItem _item;

        private PlayerCursor.PlayerCursor _cursor;
        
        public Transform Transform => transform;

        #region Actions

        public event Action<ICell, IItem, ItemSOProperty?> OnClickUpLeft;
        public event Action<ICell, IItem, ItemSOProperty?> OnClickDownLeft;
        public event Action<ICell, IItem, ItemSOProperty?> OnClickUpRight;
        public event Action<ICell, IItem, ItemSOProperty?> OnClickDownRight;
        public event Action<ICell, IItem, ItemSOProperty?> OnClickUpMiddle;
        public event Action<ICell, IItem, ItemSOProperty?> OnClickDownMiddle;

        #endregion

        public bool Equal(ItemSOProperty? itemData)
        {
            return itemData == _itemData;
        }

        public void SetCursor(PlayerCursor.PlayerCursor cursor)
        {
            _cursor = cursor;
        }

        public void Initialize(IItem template)
        {
            _item = Instantiate(template.Transform, _parent).GetComponent<IItem>();
            _item.InitializeDisplay("default Tittle", "default Description", 0, null);

            OnClickUpLeft += _cursor.OnClickUpLeft;
            OnClickDownLeft += _cursor.OnClickDownLeft;
            OnClickUpRight += _cursor.OnClickUpRight;
            OnClickDownRight += _cursor.OnClickDownRight;
            OnClickUpMiddle += _cursor.OnClickUpMiddle;
            OnClickDownMiddle += _cursor.OnClickDownMiddle;

            RandomInitItem();
        }

        public uint Put(uint maxPut)
        {
            if (_itemData == null) throw new NullReferenceException();

            uint amount = _item.Amount;
            if (maxPut >= _itemData.Value.MaxAmount - amount)
            {
                uint put = _itemData.Value.MaxAmount - amount;
                _item.Amount += _itemData.Value.MaxAmount - amount;
                return put;
            }

            _item.Amount += maxPut;
            return maxPut;
        }

        public void Put(uint put, ItemSOProperty? putObject)
        {
            if (putObject == null) throw new NullReferenceException();
            _itemData = putObject;
            _item.InitializeDisplay(putObject.Value.Tittle, putObject.Value.Description, put, putObject.Value.Icon);
        }

        public ReplaceData Take(uint maxTake)
        {
            if (_itemData == null) throw new NullReferenceException();
            if (maxTake >= _item.Amount)
            {
                uint take = _item.Amount;
                _item.Amount = 0;
                ReplaceData replaceData = new ReplaceData((ItemSOProperty)_itemData, take);
                _itemData = null;
                return replaceData;
            }

            _item.Amount -= maxTake;
            return new ReplaceData((ItemSOProperty)_itemData, maxTake);
        }

        public ReplaceData Take()
        {
            if (_itemData == null) throw new NullReferenceException();
            ReplaceData replaceData = new ReplaceData(_itemData.Value, _item.Amount);
            _item.InitializeDisplay("", "", 0, null);
            _itemData = null;
            return replaceData;
        }

        public ReplaceData Replace(ReplaceData item)
        {
            if (_itemData == null) throw new NullReferenceException();

            ReplaceData returnItem = new ReplaceData(_itemData.Value, _item.Amount);
            _itemData = item.ItemData;
            _item.InitializeDisplay(_itemData.Value.Tittle, _itemData.Value.Description, item.Amount,
                _itemData.Value.Icon);
            return returnItem;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            switch (eventData.button)
            {
                case PointerEventData.InputButton.Left:
                    OnClickUpLeft?.Invoke(this, _item, _itemData);
                    break;
                case PointerEventData.InputButton.Right:
                    OnClickUpRight?.Invoke(this, _item, _itemData);
                    break;
                case PointerEventData.InputButton.Middle:
                    OnClickUpMiddle?.Invoke(this, _item, _itemData);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            switch (eventData.button)
            {
                case PointerEventData.InputButton.Left:
                    OnClickDownLeft?.Invoke(this, _item, _itemData);
                    break;
                case PointerEventData.InputButton.Right:
                    OnClickDownRight?.Invoke(this, _item, _itemData);
                    break;
                case PointerEventData.InputButton.Middle:
                    OnClickDownMiddle?.Invoke(this, _item, _itemData);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void RandomInitItem()
        {
            if (Random.Range(0, 2) == 1)
            {
                int random = Random.Range(0, _defs.Count);
                _itemData = _defs[random].Data;
                _item.Amount = (uint)Random.Range(1, (int)_defs[random].Data.MaxAmount + 1);
                _item.Icon = _defs[random].Data.Icon;
            }
            else
            {
                _itemData = null;
            }
        }
    }
}