using UnityEngine;

[CreateAssetMenu(fileName = "new CharacterStat", menuName = "GaramStudio/Character/CharacterStat")]
public class CharacterStatSO : ScriptableObject
{
    #region Base Stats
    [Header("Base Stats")]
    public Stat strength;
    public Stat intelligence;
    public Stat vit;
    public Stat men;
    public Stat dexterity;

    #endregion

    #region Char Stats

    [Header("Stats")]
    [SerializeField, Expandable] private StatParameterSO atkParam;
    [SerializeField, ReadOnly] private Stat atk;
    [SerializeField, Expandable, Space(10)] private StatParameterSO matkParam;
    [SerializeField, ReadOnly] private Stat matk;
    [SerializeField, Expandable, Space(10)] private StatParameterSO healthParam;
    [SerializeField, ReadOnly] private Stat health;
    [SerializeField, Expandable, Space(10)] private StatParameterSO manaParam;
    [SerializeField, ReadOnly] private Stat mana;
    [SerializeField, Expandable, Space(10)] private StatParameterSO defParam;
    [SerializeField, ReadOnly] private Stat def;
    [SerializeField, Expandable, Space(10)] private StatParameterSO mdefParam;
    [SerializeField, ReadOnly] private Stat mdef;
    [SerializeField, Expandable, Space(10)] private StatParameterSO critParam;
    [SerializeField, ReadOnly] private Stat crit;
    public Stat Atk
    {
        get
        {
            atk.BaseValue = strength.Value * atkParam.multiplierValue + atkParam.baseValue;
            return atk;
        }
    }
    public Stat Matk
    {
        get
        {
            matk.BaseValue = intelligence.Value * matkParam.multiplierValue + matkParam.baseValue;
            return matk;
        }
    }
    public Stat Health
    {
        get
        {
            health.BaseValue = vit.Value * healthParam.multiplierValue + healthParam.baseValue;
            return health;
        }
    }
    public Stat Mana
    {
        get
        {
            mana.BaseValue = men.Value * manaParam.multiplierValue + manaParam.baseValue;
            return mana;
        }
    }
    public Stat Def
    {
        get
        {
            def.BaseValue = vit.Value * defParam.multiplierValue + defParam.baseValue;
            return def;
        }
    }
    public Stat Mdef
    {
        get
        {
            mdef.BaseValue = men.Value * mdefParam.multiplierValue + mdefParam.baseValue;
            return mdef;
        }
    }
    public Stat Crit
    {
        get
        {
            crit.BaseValue = dexterity.Value * critParam.multiplierValue + critParam.baseValue;
            return crit;
        }
    }
    public Stat Speed;
    public float SprintSpeed => (Speed.Value * 2) > 8 ? 8 : (Speed.Value * 2);
    public float WalkSpeed => Speed.Value > 3 ? 3 : Speed.Value;

    #endregion
}