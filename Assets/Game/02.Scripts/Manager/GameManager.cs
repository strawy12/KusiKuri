using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private ConfigDataObject configDataObject;

    public ConfigDataObject ConfigData => configDataObject;
}
