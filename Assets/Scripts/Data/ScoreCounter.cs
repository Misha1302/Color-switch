namespace Data
{
    public sealed class ScoreCounter
    {
        private int _maxScoreCount = -1;

        public int GetScoreCount(float y)
        {
            var value = (int)(y / 7);
            if (_maxScoreCount > value)
                return _maxScoreCount;

            return _maxScoreCount = value;
        }
    }
}