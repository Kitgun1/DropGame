using System;
using UnityEngine;

namespace _Project.Inventory.Structures
{
    [Serializable]
    public struct ItemSOProperty
    {
        public string Tittle;
        [TextArea] public string Description;
        public uint MaxAmount;
        public Sprite Icon;
        public float MaxDurability;

        public ItemSOProperty(
            string tittle, 
            string description, 
            uint maxAmount, 
            Sprite icon,
            float maxDurability)
        {
            Tittle = tittle;
            Description = description;
            MaxAmount = maxAmount;
            Icon = icon;
            MaxDurability = maxDurability;
        }

        #region Operations

        public static bool operator ==(ItemSOProperty o1, ItemSOProperty o2)
        {
            return o1.Tittle        == o2.Tittle &&
                   o1.Description   == o2.Description &&
                   o1.MaxAmount     == o2.MaxAmount &&
                   o1.Icon          == o2.Icon;
        }

        public static bool operator !=(ItemSOProperty o1, ItemSOProperty o2)
        {
            return !(o1.Tittle      == o2.Tittle &&
                     o1.Description == o2.Description &&
                     o1.MaxAmount   == o2.MaxAmount &&
                     o1.Icon        == o2.Icon);
        }

        public bool Equals(ItemSOProperty other)
        {
            return Tittle           == other.Tittle &&
                   Description      == other.Description &&
                   MaxAmount        == other.MaxAmount &&
                   Icon             == other.Icon;
        }

        public override bool Equals(object obj)
        {
            return obj is ItemSOProperty other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Tittle, Description, MaxAmount, Icon);
        }

        #endregion
    }
}