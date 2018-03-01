using UnityEngine;
using System.Collections;

public class Hex : MonoBehaviour
{
	public int colValue;
	public int rowValue;


	public int gCost;
	public int hCost;

	public int fCost;

	public Hex parentHex;

	public bool isInactive;

	public int FCost
	{
		get
		{
			fCost = gCost + hCost;
			return fCost;
		}
		set
		{
			fCost = value;
		}
	}

	public void SetHex(int col, int row)
	{
		colValue = col;
		rowValue = row;
	}

	public Vector2 GetHex()
	{
		return new Vector2(colValue, rowValue);
	}
}
