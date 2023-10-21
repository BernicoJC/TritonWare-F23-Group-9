using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerSpriteList : PlayerList<Sprite> { }

[Serializable]
public class PlayerColorList : PlayerList<Ref<Color>> { }

[Serializable]
public class PlayerObjectList : PlayerList<GameObject> { };

[Serializable]
public class PlayerList<T> : List<T>, ISerializationCallbackReceiver
    where T : class
{
    // The below properties aren't in an array because we need
    // custom labels, and creating a custom editor is hard.

	[SerializeField]
    private T Red;

	[SerializeField]
    private T Purple;

    public PlayerList()
    {
        for (Player p = 0; p < Player.Count; p++)
            Add(null);
    }

    public T this[Player player] { get => this[(int)player]; set => this[(int)player] = value; }

    public void OnBeforeSerialize()
	{
        if (Count != (int)Player.Count)
        {
            Clear();
            for (int i = 0; i < (int)Player.Count; i++)
                Add(null);
        }

        Red = this[Player.Red];
        Purple = this[Player.Purple];
	}
	
	public void OnAfterDeserialize()
	{
        if (Count != (int)Player.Count)
        {
            Clear();
            for (int i = 0; i < (int)Player.Count; i++)
                Add(null);
        }

		this[Player.Red] = Red;
        this[Player.Purple] = Purple;
	}
}

[Serializable]
public class Ref<T> where T : struct
{
    public T Data;

    public static implicit operator T(Ref<T> from) => from.Data;
}
