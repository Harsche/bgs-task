using UnityEngine;

public abstract class BaseCharacterAttribute : ScriptableObject
{
    [field: SerializeField] public string Name { get; protected set; }
    [field: SerializeField] public float Price { get; protected set; }
    [field: SerializeField] public Texture MaleTexture { get; protected set; }
    // It's also possible to add Female/Child/Monster texture to customize other types of characters
    [field: SerializeField] public Color Color { get; protected set; } = Color.white;

    public abstract AttributeType Type  { get; }
}

public enum AttributeType
{
    Hair,
    Torso,
    Legs,
    Feet


}