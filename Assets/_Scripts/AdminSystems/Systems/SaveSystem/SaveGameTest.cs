using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameTest : MonoBehaviour
{
    [MethodButton]
    public void SaveGame()
    {
        Admin.Global.EventSystem.GetCommandByName<EventVoid>("save_sys", "save").Invoke();
    }
}
