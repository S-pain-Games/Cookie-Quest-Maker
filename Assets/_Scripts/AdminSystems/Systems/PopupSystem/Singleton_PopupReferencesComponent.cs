using UnityEngine;

[System.Serializable]
public class Singleton_PopupReferencesComponent : MonoBehaviour
{
    public Transform m_InstantiationTransform;

    [Header("Popup Prefabs")]
    public GameObject m_PrimaryMissionPrefab;
    public GameObject m_SecondaryMissionPrefab;
    public GameObject m_GenericPrefab;
}
