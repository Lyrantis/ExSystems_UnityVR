using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField]
    public IPanel SettingsWindow;
    bool settingsMenuActive = false;

    public void ToggleSettingsMenu()
    {
        if (settingsMenuActive)
        {
            CloseMenu();
            return;
        }
        OpenMenu();
    }

    private void OpenMenu()
    {
        settingsMenuActive = true;
    }

    private void CloseMenu()
    {
        settingsMenuActive = false;
    }
}
