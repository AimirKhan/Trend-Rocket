using System.Collections.Generic;
using ScriptableObjects;
using UniRx;

namespace Services
{
    public enum EGameState
    {
        None,
        Stopped,
        Started,
        Paused,
        Continued,
        Finished
    }

    public sealed class GlobalVariables
    {
        private static GlobalVariables instance = null;
        private static readonly object padlock = new object();

        GlobalVariables()
        {
        }

        public static GlobalVariables Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new GlobalVariables();
                    }
                    return instance;
                }
            }
        }

        public ReactiveProperty<float> CurrentScore = new(0);
        public ReactiveProperty<float> RecordScore = new(0);
        public ReactiveProperty<int> CurrentZombie = new(0);
        public ReactiveProperty<EGameState> GameState = new();
        public List<ZombieSO> zombies;
    }
}
