using UnityEngine;

[CreateAssetMenu(fileName = "(Leg Attribute)", menuName = "Game/Characters/Character Attribute/Legs", order = 0)]
public class LegsAttribute : BaseCharacterAttribute
{
    public override AttributeType Type => AttributeType.Legs;
}