using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManagement : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenuAsset;

    [SerializeField]
    private GameObject optionsMenuAsset;

    [SerializeField]
    private GameObject creditsMenuAsset;

    [MethodButton]
    public void OpenOptionsMenu()
    {
        mainMenuAsset.SetActive(false);
        creditsMenuAsset.SetActive(false);
        optionsMenuAsset.SetActive(true);
    }

    [MethodButton]
    public void CloseOptionsMenu()
    {
        optionsMenuAsset.SetActive(false);
        creditsMenuAsset.SetActive(false);
        mainMenuAsset.SetActive(true);
    }

    [MethodButton]
    public void OpenCreditsMenu()
    {
        mainMenuAsset.SetActive(false);
        optionsMenuAsset.SetActive(false);
        creditsMenuAsset.SetActive(true);
    }

    [MethodButton]
    public void CloseCreditsMenu()
    {
        creditsMenuAsset.SetActive(false);
        optionsMenuAsset.SetActive(false);
        mainMenuAsset.SetActive(true);
    }

}
