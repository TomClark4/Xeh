﻿using UnityEngine;
using System.Collections;

public abstract class GenericSingleton<T> : MonoBehaviour
	where T : Component
{
	private static T instance;

	public static T Instance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<T>();

				if (instance == null)
				{
					GameObject obj = new GameObject();
					instance = obj.AddComponent<T>();
				}
			}
			return instance;
		}
	}
}
