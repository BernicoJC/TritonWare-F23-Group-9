using System;

public enum Player
{
    Yellow,
    Red,
    Count,
    None,
}

public static class PlayerExtensions
{
    public static string ToSuffix(this Player player)
    {
        switch (player)
        {
            case Player.Yellow:
                return "Y";

            case Player.Red:
                return "R";

            default:
                throw new ArgumentException("Invalid Player value", nameof(player));
        }
    }

    public static T Select<T>(this Player player, params T[] options)
    {
        return options[(int)player];
    }
}
