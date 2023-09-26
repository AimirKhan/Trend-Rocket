using System;
using UnityEngine.Events;

namespace Events
{
    public enum EGameState
    {
        Stopped,
        Started,
        Paused,
        Continued,
        Finished
    }
    
    public class GlobalEvents
    {
        private static GlobalEvents instance = null;
        private static readonly object padlock = new object();

        GlobalEvents()
        {
        }

        public static GlobalEvents Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new GlobalEvents();
                    }
                    return instance;
                }
            }
        }

        public Action<int> OnMovePlayer;
        public Action<EGameState> OnGameState = state => { };
    }
}