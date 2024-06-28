using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character Data", order = 1)]
public class AI_CharacterDefiner : ScriptableObject
{
    public string WorldDefinition;
    public string CharacterName;
    public string PublicCharacter;
    public string Abilities;
}
