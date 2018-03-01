using UnityEngine;
using System.Collections;

public class StartHex : MonoBehaviour
{
	public static Hex startHex;

	// Use this for initialization
	void Start()
	{
		
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			GetStartTile();
		}
	}
	
	void GetStartTile()
	{
		Ray startHexRay = new Ray(transform.position, -transform.up);
		RaycastHit startHexHit;
		Physics.Raycast(startHexRay, out startHexHit);

		startHex = startHexHit.collider.GetComponent<Hex>();
	}
}
