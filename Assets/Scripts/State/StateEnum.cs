namespace State
{
    public enum StateEnum
    {
        Auth = 0,
        BeforeStart = 1 << 0,
        Start = 1 << 1,
        Game = 1 << 2,
        EndOfGame = 1 << 3,
        Lose = (1 << 4) | EndOfGame,
        Win = (1 << 5) | EndOfGame
    }
}