using CQM.Databases;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseDataBuilder : MonoBehaviour
{
    public abstract void BuildData(ComponentsDatabase c);
    public abstract void LoadDataFromCode();
}
