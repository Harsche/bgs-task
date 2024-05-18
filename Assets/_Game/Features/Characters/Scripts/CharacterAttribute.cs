using UnityEngine;

[CreateAssetMenu(fileName = "(CharacterAttribute)", menuName = "Game/Characters/Character Attribute", order = 0)]
public class CharacterAttribute : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public AttributeType AttributeType { get; private set; }
    [field: SerializeField] public float Price { get; private set; }
    [field: SerializeField] public Texture Texture { get; private set; }

}

public enum AttributeType{
    Hair,
    Torso,
    Legs,
    Feet


}