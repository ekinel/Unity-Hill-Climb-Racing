using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickScript : MonoBehaviour
{
	private bool isClicked = false;

	private void OnMouseDown()
	{
		isClicked = true;
	}

	private void OnMouseUp()
	{
		isClicked = false;
	}
}
