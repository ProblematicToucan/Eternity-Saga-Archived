using UnityEngine;

[CreateAssetMenu(fileName = "new CharacterStat", menuName = "GarammStudio/Character/CharacterStat")]
public class CharacterStat : ScriptableObject
{
    #region Base Stats
    public Stat strength;
    public Stat intelligence;
    public Stat vit;
    public Stat men;
    public Stat dexterity;

    #endregion

    #region Char Stats

    public Stat atk;
    public Stat matk;
    public Stat health;
    public Stat mana;
    public Stat def;
    public Stat mdef;
    public Stat crit;
    public Stat speed;
    public float sprintSpeed => (speed.Value * 2) > 8 ? 8 : (speed.Value * 2);
    public float walkSpeed => speed.Value > 3 ? 3 : speed.Value;

    #endregion
}