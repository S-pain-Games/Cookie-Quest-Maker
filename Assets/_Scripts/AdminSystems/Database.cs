using CQM.Components;
using System.Collections.Generic;
using UnityEngine;

namespace CQM.Databases
{
    [System.Serializable]
    public class Database : MonoBehaviour
    {
        // All these references should only be called by unity objects
        // Systems should be passed the necesary data on initialization
        // This way its easier to identify what data does each system handle
        public PiecesDB Pieces { get => m_Pieces; }
        public StoryDB Stories { get => m_Stories; }
        public TownDB Town { get => m_Town; }
        public CookieDB Cookies { get => m_Cookies; }
        public DialogueDB Dialogues { get => m_Dialogues; }
        public WorldDB World { get => m_World; }
        public NpcData Npcs { get => m_Npcs; }
        public PlayerDB Player { get => m_Player; }
        public PopupDataComponent Popups { get => m_Popups; }
        public GameStateComponent GameState { get => m_GameState; }
        public CameraDataComponent Cameras { get => m_Cameras; }

        [SerializeField] private PlayerDB m_Player = new PlayerDB();
        [SerializeField] private GameStateComponent m_GameState;
        [SerializeField] private PopupDataComponent m_Popups = new PopupDataComponent();
        [SerializeField] private NpcData m_Npcs = new NpcData();
        [SerializeField] private WorldDB m_World = new WorldDB();
        [SerializeField] private DialogueDB m_Dialogues = new DialogueDB();
        [SerializeField] private CookieDB m_Cookies;
        [SerializeField] private TownDB m_Town = new TownDB();
        [SerializeField] private StoryDB m_Stories = new StoryDB();
        [SerializeField] private PiecesDB m_Pieces;
        [SerializeField] private CameraDataComponent m_Cameras = new CameraDataComponent();

        private Dictionary<int, object> m_ComponentsDic = new Dictionary<int, object>();

        public T GetComponent<T>(string query, int ID) where T : class
        {
            return m_ComponentsDic[query.GetHashCode()] as T;
        }

        public void LoadData()
        {
            m_Stories.LoadData();
            m_Pieces.LoadData();
            m_Town.LoadData(Stories);
            m_Cookies.LoadData();
            m_Dialogues.LoadData();
            m_Player.LoadData();
        }
    }
}