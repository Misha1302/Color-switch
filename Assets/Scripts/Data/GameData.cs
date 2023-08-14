using System;

[Serializable]
public class GameData
{
    public int scoreCount;

    public GameData(int scoreCount)
    {
        this.scoreCount = scoreCount;
    }
}