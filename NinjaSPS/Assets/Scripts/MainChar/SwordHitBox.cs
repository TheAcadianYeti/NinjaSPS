using UnityEngine;
using System.Collections;

public class SwordHitBox : MonoBehaviour
{
	private BoxCollider2D hitBox;
	// Use this for initialization
	void Start ()
	{
		hitBox = GetComponent<BoxCollider2D>();
	}

	public void setEnabled(bool toggleBool)
	{
		hitBox.enabled = toggleBool;
	}

	public void toggle()
	{
		hitBox.enabled = !hitBox.enabled;
	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("Yo");
	}
}
