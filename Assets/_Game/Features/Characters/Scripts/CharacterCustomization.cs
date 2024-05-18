using UnityEngine;

public class CharacterCustomization
{
    private Material _material;

    public CharacterCustomization(Material material)
    {
        _material = material;
    }

    public void SetCharacterAttribute(CharacterAttribute attribute)
    {
        int texturePropertyID = Shader.PropertyToID("_Attribute_" + attribute.AttributeType.ToString());
        _material.SetTexture(texturePropertyID, attribute.Texture);
    }
}

