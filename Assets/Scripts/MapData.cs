using UnityEngine;
using System.Collections;

[System.Serializable]
public class MapData
{
	public float x, y;

	public int sprite;
	
	public Vector2 position
	{
		get
		{
			return new Vector2(x, y);
		}
	}


	public MapData(Vector2 p, int s)
	{
		x = p.x;
		y = p.y;
		sprite = s;
	}
}
