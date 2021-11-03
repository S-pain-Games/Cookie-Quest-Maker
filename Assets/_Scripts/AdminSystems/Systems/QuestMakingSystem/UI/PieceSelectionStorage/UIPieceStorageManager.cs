using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CQM.Databases.UI
{
    // Handles over all the Storage UI functionality and
    // important data
    public class UIPieceStorageManager : MonoBehaviour
    {
        public QuestPiece.PieceType m_SelectedType;
        public int selectedStoryId; // used to populate UI with story targets

        [SerializeField] private PieceFilteringMenu _pieceFiltering;
        [SerializeField] private UIPieceSelection _pieceSelector;

        private EventSys _evtSys;

        private Event<int> _onStorySelected;
        private Event<int> _onUsePiece;

        public void Initialize(EventSys evtSys)
        {
            _evtSys = evtSys;
            _onUsePiece = _evtSys.AddEvent<int>("on_use_piece".GetHashCode());
        }

        public void AdquireUIEvents()
        {
            _evtSys.GetEvent("on_story_selected".GetHashCode(), out _onStorySelected);
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

        private void PieceFiltering_OnFilterSelected(QuestPiece.PieceType type)
        {
            m_SelectedType = type;
            _pieceSelector.Refresh(type);
        }

        private void OnStorySelected(int storyID)
        {
            _pieceSelector.m_CurrentStoryID = storyID;
        }

        // Called by the Use UI button
        private void PieceSelection_OnUsePiece(int pieceID)
        {
            _onUsePiece.Invoke(pieceID);
        }
    }
}