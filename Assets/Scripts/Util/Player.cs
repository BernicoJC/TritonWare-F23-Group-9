using System;
using System.Collections.Generic;
using System.Linq;

public enum Player
{
    Red,
    Purple,
    Count,
    None,
}

public static class PlayerExtensions
{
    public static string ToSuffix(this Player player)
    {
        switch (player)
        {
            case Player.Red:
                return "R";

            case Player.Purple:
                return "P";

            default:
                throw new ArgumentException("Invalid Player value", nameof(player));
        }
    }

    public static T Select<T>(this Player player, IEnumerable<T> options)
    {
        return options.ElementAt((int)player);
    }

    public static T SelectArg<T>(this Player player, params T[] options)
    {
        return options[(int)player];
    }
}
