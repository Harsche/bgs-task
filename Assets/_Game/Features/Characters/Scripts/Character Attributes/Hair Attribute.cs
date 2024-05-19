using UnityEngine;

[CreateAssetMenu(fileName = "(Hair Attribute)", menuName = "Game/Characters/Character Attribute/Hair", order = 0)]
public class HairAttribute : BaseCharacterAttribute
{
    public override AttributeType Type => AttributeType.Hair;
}