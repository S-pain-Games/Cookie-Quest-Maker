using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace CQM.UI.Town
{
    public class TownBuildingBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        private UITownManager _townManager;

        public void Initialize(UITownManager townManager)
        {
            _townManager = townManager;
        }

        public void OnPointerDown(PointerEventData eventData)
        {

        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Select();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Unselect();
        }

        public void OnPointerUp(PointerEventData eventData)
        {

        }

        private void Select()
        {
            _townManager.SelectTownBuilding(this);
        }

        private void Unselect()
        {
            _townManager.UnselectTownBuilding(this);
        }
    }
}