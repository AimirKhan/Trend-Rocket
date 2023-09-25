namespace Events
{
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
        
        
    }
}