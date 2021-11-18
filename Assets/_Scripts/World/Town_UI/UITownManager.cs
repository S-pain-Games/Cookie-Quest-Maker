using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CQM.UI.Town
{
    public class UITownManager : MonoBehaviour
    {
        [SerializeField] private List<TownBuildingBehaviour> m_Buildings = new List<TownBuildingBehaviour>();
        [SerializeField] private GameObject m_HappinessMeterPrefab;
    }
}