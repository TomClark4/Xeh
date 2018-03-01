using UnityEngine;
using System.Collections;

public class HexGrid : MonoBehaviour
{
	//cube to even q offset
	//col = x
	//row = z + (x + (x&1)) / 2

	//even q offset to cube
	//x = col
	//z = row - (col + (col&1)) / 2
	//y = -x-z

	public static Hex[,] hexGrid;

	public int gridSizeCol;
	public int gridSizeRow;



	// Use this for initialization
	void Start()
	{
		hexGrid = new Hex[gridSizeCol, gridSizeRow];

		PopulateHexGrid();
	}
	
	void PopulateHexGrid()
	{
		for (int col = 0; col < gridSizeCol; col++)
		{
			for (int row = 0; row < gridSizeRow; row++)
			{
				hexGrid[col, row] = transform.GetChild(col + (row * gridSizeCol)).GetComponent<Hex>();

				Hex initHex = hexGrid[col, row];
				if (initHex)
				{
					initHex.SetHex(col, row);
				}
			}
		}
	}
}
