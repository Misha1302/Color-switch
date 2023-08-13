public sealed class ScoreCounter : GameClass
{
    public int ScoreCount => (int)(GameManager.Ball.transform.position.y / 7);
}