namespace Services.RemoteConfig
{
    [System.Serializable]
    public struct Response
    {
        public InfoStruct info;
    }

    [System.Serializable]
    public struct InfoStruct
    {
        public string offer_id;
    }
}