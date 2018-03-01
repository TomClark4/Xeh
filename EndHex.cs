using UnityEngine;
using System.Collections;

public class EndHex : MonoBehaviour
{
	public static Hex endHex;

	// Use this for initialization
	void Start()
	{
		
	}
	
	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			GetEndTile();
		}
	}

	void GetEndTile()
	{
		Ray endHexRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit endHexHit;
		Physics.Raycast(endHexRay, out endHexHit);

		endHex = endHexHit.collider.GetComponent<Hex>();
	}
}
