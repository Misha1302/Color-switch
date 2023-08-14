public enum StateEnum
{
    Start = 1 << 0,
    Game = 1 << 1,
    EndOfGame = 1 << 2,
    Lose = (1 << 3) | EndOfGame,
    Win = (1 << 4) | EndOfGame
}