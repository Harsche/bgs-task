using UnityEngine;

[CreateAssetMenu(fileName = "(Torso Attribute)", menuName = "Game/Characters/Character Attribute/Torso", order = 0)]
public class TorsoAttribute : BaseCharacterAttribute
{
    public override AttributeType Type => AttributeType.Torso;
}