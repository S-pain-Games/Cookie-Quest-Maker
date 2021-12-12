using CQM.Components;
using System;

namespace CQM.Systems
{
    public class GameStateSystem : ISystemEvents
    {
        private Singleton_GameStateComponent d;

        private TransitionsSubSystem m_TransitionsSubSys;


        public void Initialize(Singleton_GameStateComponent gameState,
                               Singleton_TransitionsComponent transitions)
        {
            d = gameState;
            m_TransitionsSubSys = new TransitionsSubSystem();
            m_TransitionsSubSys.Initialize(transitions);
        }

        public void RegisterEvents(out ID sysID, out EventSys commands, out EventSys callbacks)
        {
            callbacks = new EventSys();
            commands = new EventSys();
            sysID = new ID("game_state_sys");

            var gs = new GameState(callbacks.AddEvent(new ID("bakery_enter")),
                callbacks.AddEvent(new ID("bakery_exit")),
                d.m_Bakery);
            d.m_States.Add(State.Bakery, gs);

            gs = new GameState(callbacks.AddEvent(new ID("main_menu_enter")),
                callbacks.AddEvent(new ID("main_menu_exit")),
                d.m_MainMenu);
            d.m_States.Add(State.MainMenu, gs);

            /*gs = new GameState(callbacks.AddEvent(new ID("cookie_making_enter")),
                callbacks.AddEvent(new ID("cookie_making_exit")),
                d.m_CookieMaking);
            d.m_States.Add(State.CookieMaking, gs);
            */

            gs = new GameState(callbacks.AddEvent(new ID("bakery_night_enter")),
                callbacks.AddEvent(new ID("bakery_night_exit")),
                d.m_BakeryNight);
            d.m_States.Add(State.BakeryNight, gs);

            gs = new GameState(callbacks.AddEvent(new ID("endgame_enter")),
                callbacks.AddEvent(new ID("endgame_exit")),
                d.m_EndGame);
            d.m_States.Add(State.EndGame, gs);

            var evt = commands.AddEvent<State>(new ID("set_game_state"));
            evt.OnInvoked += SetState;
        }

        public void StartGame()
        {
            // Disable All States Except for the starting one (Maybe Bug prone?)
            foreach (GameState state in d.m_States.Values)
            {
                state.DisableAllGameobjects();
            }
            d.m_State = d.m_States[State.MainMenu];
            d.m_State.OnStateEnter();
        }

        public void SetState(State state)
        {
            d.m_GameplayStarted = true;
            m_TransitionsSubSys.TransitionTo(() =>
            {
                d.m_State.OnStateExit();
                d.m_State = d.m_States[state];
                if (state != State.MainMenu)
                    d.m_GameplayState = state;
                d.m_State.OnStateEnter();
            });
        }


        public class TransitionsSubSystem
        {
            private Singleton_TransitionsComponent _data;

            public void Initialize(Singleton_TransitionsComponent component)
            {
                _data = component;
            }

            public void TransitionTo(Action midPoint)
            {
                var t = _data.m_FadeTransition;
                t.outCompleted += midPoint;
                t.outCompleted += () => _data.m_FadeTransition.TransitionIn(); ;
                t.inCompleted += () => t.ClearAllCallbacks();
                t.TransitionOut();
            }
        }


        [Serializable]
        public class Singleton_TransitionsComponent
        {
            public TransitionBehaviour m_FadeTransition;
        }


        public enum State
        {
            MainMenu,
            Bakery,
            CookieMaking,
            BakeryNight,
            EndGame
        }
    }
}