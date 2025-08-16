using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoticeData
{
    [TextArea(1, 3)]
    public string head;
    public Sprite headIcon;
    public Color headColor;
    public Color headBGColor;
    [TextArea(5, 30)]
    public string body;
    public Sprite bodyIcon;
    public Color bodyColor;
    public Color bodyBGColor;
}

[CreateAssetMenu(fileName = nameof(ConfigDataObject), menuName = "SO/NoticeData")]

public class NoticeDataObject : ScriptableObject
{
    [SerializeField] private List<NoticeData> noticeDataList;
}
