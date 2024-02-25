

using UI;

namespace Player
{
    [System.Serializable]
    public class Player
    {
        public int HeroId { get; private set; }
        public PositionOnTheScreen ScreenPosition { get; private set; }
        public int Score { get; set; }

        public Player(int heroId, PositionOnTheScreen screenPosition, int score = 0)
        {
            this.HeroId = heroId;
            this.ScreenPosition = screenPosition;
            this.Score = score;
        }
    }
}
