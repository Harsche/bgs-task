using UnityEngine;

[CreateAssetMenu(fileName = "(Feet Attribute)", menuName = "Game/Characters/Character Attribute/Feet", order = 0)]
public class FeetAttribute : BaseCharacterAttribute
{
    public override AttributeType Type => AttributeType.Feet;
}