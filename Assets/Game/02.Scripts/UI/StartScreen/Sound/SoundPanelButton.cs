using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SoundPanelButton : HighlightButton
{
    public SoundPanel soundPanel;

    private void Start()
    {
        OnClick += OpenSoundPanel;
    }

    public void OpenSoundPanel()
    {

        if (soundPanel.GetComponent<CanvasGroup>().interactable == true)
        {
            soundPanel.Close();
        }
        else
        {
            soundPanel.OpenPanel();
        }
    }
}
