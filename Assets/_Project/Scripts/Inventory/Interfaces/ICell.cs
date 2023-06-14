using System;
using _Project.Inventory.Structures;
using UnityEngine;

namespace _Project.Inventory.Interfaces
{
    public interface ICell
    {
        public Transform Transform { get; }

        /// <summary>
        /// Called when the user pressed the inventory cell with the left mouse button.
        /// </summary>
        public event Action<ICell, IItem, ItemSOProperty?> OnClickUpLeft;

        /// <summary>
        /// Called when the user clicked on the inventory cell with the left mouse button.
        /// </summary>
        public event Action<ICell, IItem, ItemSOProperty?> OnClickDownLeft;

        /// <summary>
        /// Called when the user pressed the inventory cell with the right mouse button.
        /// </summary>
        public event Action<ICell, IItem, ItemSOProperty?> OnClickUpRight;

        /// <summary>
        /// Called when the user right-clicked on an inventory cell.
        /// </summary>
        public event Action<ICell, IItem, ItemSOProperty?> OnClickDownRight;

        /// <summary>
        /// Called when the user pressed the middle mouse button on the inventory cell.
        /// </summary>
        public event Action<ICell, IItem, ItemSOProperty?> OnClickUpMiddle;

        /// <summary>
        /// Called when the user clicked on the inventory cell with the middle mouse button.
        /// </summary>
        public event Action<ICell, IItem, ItemSOProperty?> OnClickDownMiddle;

        public bool Equal(ItemSOProperty? itemData);

        public void SetCursor(PlayerCursor.PlayerCursor cursor);
        
        public void Initialize(IItem template);

        public uint Put(uint maxPut);
        public void Put(uint put, ItemSOProperty? putObject);

        public ReplaceData Take(uint maxTake);
        public ReplaceData Take();

        public ReplaceData Replace(ReplaceData item);
    }
}