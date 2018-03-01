using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinder : MonoBehaviour
{
	public float speed;

	Vector3 endHexPos;

	public List<Hex> openSet;

	public List<Hex> closedSet;

	public List<Hex> neighbourHexes;

	public List<Hex> hexPath = new List<Hex>();
		
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Pathfind();
		}

		if (hexPath.Count != 0)
		{
			Movement();
		}
	}

	void Pathfind()
	{
		for (int i = 0; i < openSet.Count; i++)
		{
			openSet[i].gCost = 0;
			openSet[i].hCost = 0;
		}

		for (int i = 0; i < closedSet.Count; i++)
		{
			closedSet[i].gCost = 0;
			closedSet[i].hCost = 0;
		}

		openSet.Clear();
		closedSet.Clear();
		neighbourHexes.Clear();
		hexPath.Clear();

		openSet = new List<Hex>();
		closedSet = new List<Hex>();

		openSet.Add(StartHex.startHex);

		while (openSet.Count > 0)
		{
			Hex currentHex = openSet[0];

			GetLowestFCostHex(ref currentHex);

			openSet.Remove(currentHex);
			closedSet.Add(currentHex);

			if (currentHex == EndHex.endHex)
			{
				GetCalculatedPath(StartHex.startHex, EndHex.endHex);
				return;
			}

			neighbourHexes = new List<Hex>();

			PopulateNeighbourHexes(currentHex);

			CalculateNewHexCosts(currentHex);
		}
	}

	void GetLowestFCostHex(ref Hex currentHex)
	{
		for (int i = 1; i < openSet.Count; i++)
		{
			if (openSet[i].FCost < currentHex.FCost || openSet[i].FCost == currentHex.FCost && openSet[i].hCost < currentHex.hCost)
			{
				currentHex = openSet[i];
			}
		}
	}

	void GetCalculatedPath(Hex startHex, Hex endHex)
	{
		Hex currentHex = endHex;

		while (currentHex != startHex)
		{
			hexPath.Add(currentHex);
			currentHex = currentHex.parentHex;
		}

		hexPath.Add(StartHex.startHex);

		hexPath.Reverse();
	}

	void PopulateNeighbourHexes(Hex currentHex)
	{
		Hex oneHex;
		Hex twoHex;
		Hex threeHex;
		Hex fourHex;
		Hex fiveHex;
		Hex sixHex;

		//if row is 0 and column even dont do hex above
		//if row is 0 and column odd dont do 3 hexes above
		//if row is max and column even dont do 3 hexes below
		//if row is max and column odd dont do hex below
		//if column is 0 dont do 2 hexes left
		//if column is max dont do 2 hexes right

		if (currentHex.GetHex().x % 2 == 0)
		{
			if (currentHex.colValue != HexGridManager.Instance.listOfHexGrids[0].gridSizeCol - 1 && currentHex.rowValue != HexGridManager.Instance.listOfHexGrids[0].gridSizeRow - 1)
			{
				oneHex = HexGrid.hexGrid[(int)currentHex.GetHex().x + 1, (int)currentHex.GetHex().y + 1];
				neighbourHexes.Add(oneHex);
			}

			if (currentHex.colValue != HexGridManager.Instance.listOfHexGrids[0].gridSizeCol - 1)
			{
				twoHex = HexGrid.hexGrid[(int)currentHex.GetHex().x + 1, (int)currentHex.GetHex().y];
				neighbourHexes.Add(twoHex);
			}

			if (currentHex.rowValue != 0)
			{
				threeHex = HexGrid.hexGrid[(int)currentHex.GetHex().x, (int)currentHex.GetHex().y - 1];
				neighbourHexes.Add(threeHex);
			}

			if (currentHex.colValue != 0)
			{
				fourHex = HexGrid.hexGrid[(int)currentHex.GetHex().x - 1, (int)currentHex.GetHex().y];
				neighbourHexes.Add(fourHex);
			}

			if (currentHex.colValue != 0 && currentHex.rowValue != HexGridManager.Instance.listOfHexGrids[0].gridSizeRow - 1)
			{
				fiveHex = HexGrid.hexGrid[(int)currentHex.GetHex().x - 1, (int)currentHex.GetHex().y + 1];
				neighbourHexes.Add(fiveHex);
			}

			if (currentHex.rowValue != HexGridManager.Instance.listOfHexGrids[0].gridSizeRow - 1)
			{
				sixHex = HexGrid.hexGrid[(int)currentHex.GetHex().x, (int)currentHex.GetHex().y + 1];
				neighbourHexes.Add(sixHex);
			}

			Debug.Log("even");
		}

		else if (currentHex.GetHex().x % 2 == 1)
		{
			if (currentHex.colValue != HexGridManager.Instance.listOfHexGrids[0].gridSizeCol - 1)
			{
				oneHex = HexGrid.hexGrid[(int)currentHex.GetHex().x + 1, (int)currentHex.GetHex().y];
				neighbourHexes.Add(oneHex);
			}

			if (currentHex.colValue != HexGridManager.Instance.listOfHexGrids[0].gridSizeCol - 1 && currentHex.rowValue != 0)
			{
				twoHex = HexGrid.hexGrid[(int)currentHex.GetHex().x + 1, (int)currentHex.GetHex().y - 1];
				neighbourHexes.Add(twoHex);
			}

			if (currentHex.rowValue != 0)
			{
				threeHex = HexGrid.hexGrid[(int)currentHex.GetHex().x, (int)currentHex.GetHex().y - 1];
				neighbourHexes.Add(threeHex);
			}

			if (currentHex.rowValue != 0)
			{
				fourHex = HexGrid.hexGrid[(int)currentHex.GetHex().x - 1, (int)currentHex.GetHex().y - 1];
				neighbourHexes.Add(fourHex);
			}

			fiveHex = HexGrid.hexGrid[(int)currentHex.GetHex().x - 1, (int)currentHex.GetHex().y];
			neighbourHexes.Add(fiveHex);

			if (currentHex.rowValue != HexGridManager.Instance.listOfHexGrids[0].gridSizeRow - 1)
			{
				sixHex = HexGrid.hexGrid[(int)currentHex.GetHex().x, (int)currentHex.GetHex().y + 1];
				neighbourHexes.Add(sixHex);
			}

			Debug.Log("odd");
		}
	}

	//not the same sign, add them
	//same sign, largest one
	void CalculateNewHexCosts(Hex currentHex)
	{
		for (int i = 0; i < neighbourHexes.Count; i++)
		{
			if (neighbourHexes[i].isInactive || closedSet.Contains(neighbourHexes[i]))
			{
				continue;
			}
			int newMovementCostGX = Mathf.Abs((int)currentHex.GetHex().x - (int)neighbourHexes[i].GetHex().x);
			int newMovementCostGY = Mathf.Abs((int)currentHex.GetHex().y - (int)neighbourHexes[i].GetHex().y);

			int movementCostToAddG = newMovementCostGX;
			if (newMovementCostGY > movementCostToAddG)
			{
				movementCostToAddG = newMovementCostGY;
			}

			int newMovementCost = currentHex.gCost + movementCostToAddG;
			if (newMovementCost < neighbourHexes[i].gCost || !openSet.Contains(neighbourHexes[i]))
			{
				neighbourHexes[i].gCost = newMovementCost;

				int newMovementCostHX = Mathf.Abs((int)EndHex.endHex.GetHex().x - (int)neighbourHexes[i].GetHex().x);
				int newMovementCostHY = Mathf.Abs((int)EndHex.endHex.GetHex().y - (int)neighbourHexes[i].GetHex().y);

				int movementCostToAddH = newMovementCostHX;
				if (newMovementCostHY > movementCostToAddH)
				{
					movementCostToAddH = newMovementCostHY;
				}

				neighbourHexes[i].hCost = movementCostToAddH;
				neighbourHexes[i].parentHex = currentHex;

				if (!openSet.Contains(neighbourHexes[i]))
				{
					openSet.Add(neighbourHexes[i]);
				}
			}
		}
	}













	void Movement()
	{
		endHexPos = EndHex.endHex.transform.position;
		transform.position = Vector3.MoveTowards(transform.position, new Vector3(hexPath[0].transform.position.x, transform.position.y, hexPath[0].transform.position.z), speed * Time.deltaTime);

		if (transform.position == new Vector3(hexPath[0].transform.position.x, transform.position.y, hexPath[0].transform.position.z))
		{
			hexPath.RemoveAt(0);
		}
	}
}
