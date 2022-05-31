namespace LoLSSTracker
{
    public class EnemyPlayer
    {
        public string champImageURL { get; set; }
        public string spell1URL { get; set; }
        public int spell1Cooldown { get; set; }
        public string spell2URL { get; set; }
        public int spell2Cooldown { get; set; }

        public int playerNumber { get; set; }
    }
}