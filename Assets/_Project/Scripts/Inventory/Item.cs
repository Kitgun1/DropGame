using _Project.Inventory.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Inventory
{
    public class Item : MonoBehaviour, IItem
    {
        [SerializeField] private TMP_Text _tittle;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private TMP_Text _amount;
        [SerializeField] private Image _icon;

        public Transform Transform => transform;
        public RectTransform RectTransform { get; private set; }

        public string Tittle
        {
            get => _tittle != null ? _tittle.text : "";
            set
            {
                if (_tittle != null) _tittle.text = value;
            }
        }

        public string Description
        {
            get => _description != null ? _description.text : "";
            set
            {
                if (_description != null) _description.text = value;
            }
        }

        public uint Amount
        {
            get
            {
                if (_amount != null)
                {
                    return uint.Parse(_amount.text);
                }

                return 0;
            }
            set
            {
                if (_amount != null) _amount.text = value.ToString();
                _amount.enabled = value != 0;
                if (Amount != 0) return;
                Tittle = "";
                Description = "";
                Icon = null;
            }
        }

        public Sprite Icon
        {
            get => _icon != null ? _icon.sprite : null;
            set
            {
                if (_icon == null) return;
                _icon.sprite = value;
                _icon.enabled = value != null;
            }
        }

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
        }

        public void InitializeDisplay(string tittle, string description, uint amount, Sprite icon)
        {
            Tittle = tittle;
            Description = description;
            Amount = amount;
            Icon = icon;
        }
    }
}