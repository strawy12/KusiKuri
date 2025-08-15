using UnityEngine;

[CreateAssetMenu(fileName = nameof(ConfigDataObject), menuName = "SO/ConfigData")]
public class ConfigDataObject : ScriptableObject
{
    public int baseTouchPerMoney = 1;

    [Header("크리티컬")]
    [Range(0f, 1f), ]
    public float criticalChance = 0.05f;
    public decimal criticalMultiplier = 2m;

    [Header("탭 제한")]
    public int maxTapsPerSecond = 120;
}
