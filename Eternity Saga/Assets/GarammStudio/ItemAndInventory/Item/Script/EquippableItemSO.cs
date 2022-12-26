using UnityEngine;

[CreateAssetMenu(fileName = "new Equipment Item", menuName = "GarammStudio/Item/Equipment Item")]
public class EquippableItemSO : ItemSO
{
    [field: SerializeField] public ItemType ItemType { get; private set; }
    [field: SerializeField] public EquippableItemType EquipType { get; private set; }

    #region Flat
    [field: SerializeField, Header("Flat Value")] public int StrengthBonus { get; private set; }
    [field: SerializeField] public int IntelligenceBonus { get; private set; }
    [field: SerializeField] public int VitBonus { get; private set; }
    [field: SerializeField] public int MenBonus { get; private set; }
    [field: SerializeField] public int DexterityBonus { get; private set; }
    #endregion

    #region Percent
    [field: SerializeField, Header("Percent Value")] public float atkBonusPercent { get; private set; }
    [field: SerializeField] public float matkBonusPercent { get; private set; }
    [field: SerializeField] public float healthBonusPercent { get; private set; }
    [field: SerializeField] public float manaBonusPercent { get; private set; }
    [field: SerializeField] public float defBonusPercent { get; private set; }
    [field: SerializeField] public float mdefkBonusPercent { get; private set; }
    [field: SerializeField] public float critkBonusPercent { get; private set; }
    [field: SerializeField] public float speedBonusPercent { get; private set; }
    #endregion

    private void Awake()
    {
        ItemType = ItemType.Equipment;
    }

    public void Equip(CharacterStat characterStat)
    {
        #region FlatModifier
        if (StrengthBonus != 0)
            characterStat.strength.AddModifier(new StatModifier(StrengthBonus, StatModType.Flat, this));
        if (IntelligenceBonus != 0)
            characterStat.intelligence.AddModifier(new StatModifier(IntelligenceBonus, StatModType.Flat, this));
        if (VitBonus != 0)
            characterStat.vit.AddModifier(new StatModifier(VitBonus, StatModType.Flat, this));
        if (MenBonus != 0)
            characterStat.men.AddModifier(new StatModifier(MenBonus, StatModType.Flat, this));
        if (DexterityBonus != 0)
            characterStat.dexterity.AddModifier(new StatModifier(DexterityBonus, StatModType.Flat, this));
        #endregion

        #region PercentModifier
        if (atkBonusPercent != 0)
            characterStat.Atk.AddModifier(new StatModifier(atkBonusPercent, StatModType.PercentAdd, this));
        if (matkBonusPercent != 0)
            characterStat.Matk.AddModifier(new StatModifier(matkBonusPercent, StatModType.PercentAdd, this));
        if (healthBonusPercent != 0)
            characterStat.Health.AddModifier(new StatModifier(healthBonusPercent, StatModType.PercentAdd, this));
        if (manaBonusPercent != 0)
            characterStat.Mana.AddModifier(new StatModifier(manaBonusPercent, StatModType.PercentAdd, this));
        if (defBonusPercent != 0)
            characterStat.Def.AddModifier(new StatModifier(defBonusPercent, StatModType.PercentAdd, this));
        if (mdefkBonusPercent != 0)
            characterStat.Mdef.AddModifier(new StatModifier(mdefkBonusPercent, StatModType.PercentAdd, this));
        if (critkBonusPercent != 0)
            characterStat.Crit.AddModifier(new StatModifier(critkBonusPercent, StatModType.PercentAdd, this));
        if (speedBonusPercent != 0)
            characterStat.Speed.AddModifier(new StatModifier(speedBonusPercent, StatModType.PercentAdd, this));
        #endregion
    }

    public void Unequip(CharacterStat characterStat)
    {
        characterStat.strength.RemoveAllModifiersFromSource(this);
        characterStat.intelligence.RemoveAllModifiersFromSource(this);
        characterStat.vit.RemoveAllModifiersFromSource(this);
        characterStat.men.RemoveAllModifiersFromSource(this);
        characterStat.dexterity.RemoveAllModifiersFromSource(this);
        characterStat.Atk.RemoveAllModifiersFromSource(this);
        characterStat.Matk.RemoveAllModifiersFromSource(this);
        characterStat.Health.RemoveAllModifiersFromSource(this);
        characterStat.Mana.RemoveAllModifiersFromSource(this);
        characterStat.Def.RemoveAllModifiersFromSource(this);
        characterStat.Mdef.RemoveAllModifiersFromSource(this);
        characterStat.Crit.RemoveAllModifiersFromSource(this);
        characterStat.Speed.RemoveAllModifiersFromSource(this);
    }
}

public enum EquippableItemType
{
    Weapon,
    Helmet,
    Armor,
    Boots,
    Accessory,
}