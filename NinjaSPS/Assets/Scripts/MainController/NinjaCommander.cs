using UnityEngine;
using System.Collections;

public class NinjaCommander : MonoBehaviour {

	public GameObject ninja;
	public int spawnDelay = 200, MAX_NINJAS = 4;
	private Ninja[] ninjas;
	private int  spawnCount = 0, currentSpawnDelay;

	
	// Use this for initialization
	void Start ()
	{
		currentSpawnDelay = spawnDelay;
		ninjas = new Ninja[MAX_NINJAS];
		GameObject[] otherOnes = GameObject.FindGameObjectsWithTag("Ninja");
		for(int i = 0; i < MAX_NINJAS; i++)
		{
			//ninjas[i] = otherOnes[i].GetComponent<Ninja>();
			ninjas[i] = null;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetButtonDown("Attack"))
		{
			for(int i = 0; i < ninjas.Length; i++)
			{
				if (ninjas[i] != null)
				{
					ninjas[i].Attack();
				}
			}
		}

		if(--currentSpawnDelay<= 0 && spawnCount < MAX_NINJAS)
		{
			//Spawn a ninja
			Debug.Log("What");
			ninjas[spawnCount++] = ((GameObject)Instantiate(ninja, transform.position, Quaternion.identity)).GetComponent<Ninja>();
			currentSpawnDelay = spawnDelay;
		}
		
	}
}
