using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGetValueTest : MonoBehaviour
{
    public Quest quest;

    [MethodButton]
    public void LogValue()
    {
        quest.GetOverallTag(out var t1, out int t1v);
        quest.GetOverallTagV2(out var t2, out int t2v);

        Debug.Log($"V1 : {t1} -> {t1v}");
        Debug.Log($"V2 : {t2} -> {t2v}");
    }
}
