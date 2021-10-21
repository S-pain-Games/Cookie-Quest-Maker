using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGetValueTest : MonoBehaviour
{
    public QuestData quest;
    private QuestSystem qs = new QuestSystem();

    [MethodButton]
    public void LogValue()
    {
        qs.GetOverallTag(quest.m_PiecesList, out var t1, out int t1v);
        //qs.GetOverallTag(quest.m_PiecesList, out var t2, out int t2v);

        Debug.Log($"V1 : {t1} -> {t1v}");
        //Debug.Log($"V2 : {t2} -> {t2v}");
    }
}
