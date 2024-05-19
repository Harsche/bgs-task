using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour {
    [SerializeField] private Image _icon;
    [SerializeField] private Button _button;
    private BaseCharacterAttribute _attribute;

    public Button Button => _button;
    
    public void SetItem(BaseCharacterAttribute attribute){
        _attribute = attribute;
        _icon.sprite = _attribute.Icon;
    }
}