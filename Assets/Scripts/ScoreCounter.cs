public sealed class ScoreCounter : GameClass
{
    private int _maxScoreCount = -1;

    public int ScoreCount => GetScoreCount();

    private int GetScoreCount()
    {
        var value = (int)(GameManager.Ball.transform.position.y / 7);
        if (_maxScoreCount > value)
            return _maxScoreCount;

        return _maxScoreCount = value;
    }
}