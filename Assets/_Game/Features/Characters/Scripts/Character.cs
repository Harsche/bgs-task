using UnityEngine;

public class Character : MonoBehaviour
{

    [SerializeField] protected SpriteRenderer _spriteRenderer;
    [SerializeField] protected Animator _animator;

    [Header("Attributes")]
    [SerializeField] protected HairAttribute _hair;
    [SerializeField] protected Color _hairColor = Color.white;
    [SerializeField] protected TorsoAttribute _torso;
    [SerializeField] protected Color _torsoColor = Color.white;
    [SerializeField] protected LegsAttribute _legs;
    [SerializeField] protected Color _legsColor = Color.white;
    [SerializeField] protected FeetAttribute _feet;
    [SerializeField] protected Color _feetColor = Color.white;

    protected Material _material;

    protected virtual void OnValidate()
    {
        if (_material == null)
        {
            _material = new Material(Shader.Find("Shader Graphs/Character"));
            _spriteRenderer.material = _material;
        }

        if (_spriteRenderer != null)
        {
            UpdateAttributes();
        }
    }

    public void SetCharacterAttribute(BaseCharacterAttribute attribute)
    {
        int texturePropertyID = Shader.PropertyToID($"_Attribute_{attribute.Type}");
        int colorPropertyID = Shader.PropertyToID($"_{attribute.Type}Color");
        _material.SetTexture(texturePropertyID, attribute.MaleTexture);
        _material.SetColor(colorPropertyID, GetAttributeColor(attribute));

    }

    public void RemoveCharacterAttribute(AttributeType attributeType)
    {
        int texturePropertyID = Shader.PropertyToID("_Attribute_" + attributeType.ToString());
        _material.SetTexture(texturePropertyID, null);
    }

    public void UpdateAttributes()
    {
        if (_hair) { SetCharacterAttribute(_hair); }
        else { RemoveCharacterAttribute(AttributeType.Hair); }

        if (_torso) { SetCharacterAttribute(_torso); }
        else { RemoveCharacterAttribute(AttributeType.Torso); }

        if (_legs) { SetCharacterAttribute(_legs); }
        else { RemoveCharacterAttribute(AttributeType.Legs); }

        if (_feet) { SetCharacterAttribute(_feet); }
        else { RemoveCharacterAttribute(AttributeType.Feet); }
    }

    [ContextMenu("Reset Material")]
    private void ResetMaterial()
    {
        _material = new Material(Shader.Find("Shader Graphs/Character"));
        _spriteRenderer.material = _material;

        if (_spriteRenderer != null)
        {
            UpdateAttributes();
        }
    }

    private Color GetAttributeColor(BaseCharacterAttribute attribute)
    {
        bool isColorCustom = false;
        switch (attribute.Type)
        {
            case AttributeType.Hair:
                isColorCustom = _hairColor != Color.white;
                return isColorCustom ? _hairColor : attribute.Color;
            case AttributeType.Torso:
                isColorCustom = _torsoColor != Color.white;
                return isColorCustom ? _torsoColor : attribute.Color;
            case AttributeType.Legs:
                isColorCustom = _legsColor != Color.white;
                return isColorCustom ? _legsColor : attribute.Color;
            case AttributeType.Feet:
                isColorCustom = _feetColor != Color.white;
                return isColorCustom ? _feetColor : attribute.Color;
            default:
                Debug.LogError("Type is not defined.");
                return Color.white;
        }
    }
}