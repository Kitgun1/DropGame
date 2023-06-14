using UnityEngine;

namespace _Project.Inventory.Interfaces
{
    public interface IItem
    {
        public string Tittle { get; set; }
        public string Description { get; set; }
        public uint Amount { get; set; }
        public Sprite Icon { get; set; }
        public Transform Transform { get; }
        public RectTransform RectTransform { get; }

        public void InitializeDisplay(string tittle, string description, uint amount, Sprite icon);
    }
}