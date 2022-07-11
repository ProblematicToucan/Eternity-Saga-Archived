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

    #endregion
}