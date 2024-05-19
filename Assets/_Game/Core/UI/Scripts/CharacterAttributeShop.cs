using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterAttributeShop : MonoBehaviour
{
    [SerializeField] private List<BaseCharacterAttribute> _shopItems;
    [SerializeField] private ShopButton _shopButtonPrefab;
    [SerializeField] private Transform _buttonsParent;
    [SerializeField] private Button  _buyButton;
    [SerializeField] private Button  _sellButton;
    [SerializeField] private Button  _equipButton;

    [Header("Item Details")]
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _purchased;

    [Header("Sounds")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _selectSfx;
    [SerializeField] private AudioClip _buySfx;
    [SerializeField] private AudioClip _sellSfx;
    [SerializeField] private AudioClip _equipSfx;

    private BaseCharacterAttribute _selectedItem;

    private void Awake()
    {
        foreach (BaseCharacterAttribute attribute in _shopItems)
        {
            ShopButton button = Instantiate(_shopButtonPrefab, _buttonsParent);
            button.SetItem(attribute);
            button.Button.onClick.AddListener(() => SelectItem(attribute));
        }
    }

    public void SelectItem(BaseCharacterAttribute attribute)
    {
        _selectedItem = attribute;
        _icon.sprite = _selectedItem.Icon;
        _price.text = $"{_selectedItem.Price}";
    }

    public void BuyItem(){
        
    }

    public void SellItem(){
        
    }

    public void EquipItem(){
        
    }
}
