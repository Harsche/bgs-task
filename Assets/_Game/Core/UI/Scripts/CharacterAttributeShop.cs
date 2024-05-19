using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class CharacterAttributeShop : MonoBehaviour
{
    [SerializeField] private List<BaseCharacterAttribute> _shopItems;
    [SerializeField] private ShopButton _shopButtonPrefab;
    [SerializeField] private Transform _buttonsParent;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _sellButton;
    [SerializeField] private Button _equipButton;
    [SerializeField] private TMP_Text _playerCoins;

    [Header("Animation")]
    [SerializeField] private RectTransform _contentTransform;
    [SerializeField] private Vector2 _showPosition;
    [SerializeField] private Vector2 _hidePosition;
    [SerializeField] private float _toggleDuration = 0.5f;

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
    private Canvas _canvas;

    private void Awake()
    {
        _contentTransform.anchoredPosition = _hidePosition;
        _canvas = GetComponent<Canvas>();
        _canvas.enabled = false;

        foreach (BaseCharacterAttribute attribute in _shopItems)
        {
            ShopButton button = Instantiate(_shopButtonPrefab, _buttonsParent);
            button.SetItem(attribute);
            button.Button.onClick.AddListener(() => SelectItem(attribute));
        }
    }

    public void Toggle(bool value)
    {
        if (value)
        {
            _canvas.enabled = true;
            _contentTransform.DOAnchorPos(_showPosition, _toggleDuration);
        }
        else
        {
            _contentTransform.DOAnchorPos(_hidePosition, _toggleDuration)
                .OnComplete(() => _canvas.enabled = false);
        }
    }

    public void SelectItem(BaseCharacterAttribute attribute)
    {
        _selectedItem = attribute;
        _audioSource.PlayOneShot(_selectSfx);
        UpdateDetails();
    }

    public void BuyItem()
    {
        Player.Instance.Coins -= _selectedItem.Price;
        _selectedItem.Purchased = true;
        _audioSource.PlayOneShot(_buySfx);
        UpdateDetails();
    }

    public void SellItem()
    {
        Player.Instance.Coins += _selectedItem.Price;
        Player.Instance.RemoveCharacterAttributeIfEquipped(_selectedItem);
        _selectedItem.Purchased = false;
        _audioSource.PlayOneShot(_sellSfx);
        UpdateDetails();
    }

    public void EquipItem()
    {
        Player.Instance.SetCharacterAttribute(_selectedItem);
        _audioSource.PlayOneShot(_equipSfx);
    }

    private void UpdateDetails()
    {
        _name.text = _selectedItem.Name;
        _icon.gameObject.SetActive(true);
        _icon.sprite = _selectedItem.Icon;
        _price.text = $"{_selectedItem.Price}";
        _purchased.gameObject.SetActive(_selectedItem.Purchased);
        _buyButton.interactable = !_selectedItem.Purchased;
        _sellButton.interactable = _selectedItem.Purchased;
        _equipButton.interactable = _selectedItem.Purchased;
        _playerCoins.text = Player.Instance.Coins.ToString();
    }
}
