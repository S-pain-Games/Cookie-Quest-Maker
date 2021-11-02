using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T>
{
    protected static T ms_Inst;

    public static T GetInst()
    {
        return ms_Inst;
    }
}