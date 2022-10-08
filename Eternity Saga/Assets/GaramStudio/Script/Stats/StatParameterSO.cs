using UnityEngine;


[CreateAssetMenu(fileName = "new StatParameter", menuName = "GaramStudio/Character/StatParameter")]
public class StatParameterSO : ScriptableObject
{
    public float baseValue;
    public float multiplierValue;
}