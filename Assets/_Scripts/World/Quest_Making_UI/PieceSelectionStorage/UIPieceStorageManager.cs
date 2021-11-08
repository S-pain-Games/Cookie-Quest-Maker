using CQM.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CQM.Gameplay
{
    // Handles over all the Storage UI functionality and
    // important data
    public class UIPieceStorageManager : MonoBehaviour
    {
        public QuestPieceFunctionalComponent.PieceType m_SelectedType;
        public int selectedStoryId; // used to populate UI with story targets

        [SerializeField] private PieceFilteringMenu _pieceFiltering;
        [SerializeField] private UIPieceSelection _pieceSelector;

        private EventSys _evtSys;

        private Event<ID> _onStorySelected;
        private Event<ID> _onUsePiece;

        public void Initialize(EventSys evtSys)
        {
            _evtSys = evtSys;
            _onUsePiece = _evtSys.AddEvent<ID>(new ID("on_use_piece"));
        }

        public void AdquireUIEvents()
        {
            _evtSys.GetEvent(new ID("on_story_selected"), out _onStorySelected);
            _onStorySelected.OnInvoked += OnStorySelected;
        }

        private void OnEnable()
        {
            _pieceFiltering.OnFilterSelected += PieceFiltering_OnFilterSelected;
            _pieceSelector.OnUsePiece += PieceSelection_OnUsePiece;
        }

        private void OnDisable()
        {
            _pieceFiltering.OnFilterSelected -= PieceFiltering_OnFilterSelected;
            _pieceSelector.OnUsePiece -= PieceSelection_OnUsePiece;
        }

        private void PieceFiltering_OnFilterSelected(QuestPieceFunctionalComponent.PieceType type)
        {
            m_SelectedType = type;
            _pieceSelector.Refresh(type);
        }

        private void OnStorySelected(ID storyID)
        {
            _pieceSelector.m_CurrentStoryID = storyID;
        }

        // Called by the Use UI button
        private void PieceSelection_OnUsePiece(ID pieceID)
        {
            _onUsePiece.Invoke(pieceID);
        }
    }
}