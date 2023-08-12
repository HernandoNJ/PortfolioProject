using System.Collections.Generic;
using UnityEngine;

public class CheckpointsManager : MonoBehaviour
{
	private GameObject[] checkpoints;

	private List<int> numbersList1;
	private List<int> numbersList2;

	void Start()
	{
		// Create an array with all GOs with tag "Checkpoint"
		checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
		SetValidCheckpoint();
	}

	private void SetValidCheckpoint()
	{
		// create Lists objects
		numbersList1 = new List<int>();
		numbersList2 = new List<int>(); //list with selected numbers

		// Add numbers to numbersList1 using GOs array.Length (checkpoints.Length)
		// checkpoints.Length is the number of total checkpoints
		for (int i = 0; i < checkpoints.Length; i++)
			numbersList1.Add(i);

		// *for loop used to add numbers to numbersList2 and checkout values
		// numbersList2 keeps the valid checkpoints values
		// if the random value is repeated (if it's already contained in numbersList2)
		// it is not added, and the loop counter is not increased

		int n = 4; // number of valid (desired) checkpoints
		for (int i = 0; i < n;) // i++ is applied only if the number is not repeated
		{
			int randNum = Random.Range(0, numbersList1.Count); // just for checking in console ... Debug.Log("choosen number: " + randNum);

			if (!numbersList2.Contains(randNum)) // if the list2 doesn't contain the randNum
			{
				numbersList2.Add(randNum); // needed because we need to check out if number is repeated
				checkpoints[randNum].tag = "ValidCheckpoint"; // change GO tag
				i++; // increase the for loop counter
			}
			else // if the number is repeated
				Debug.Log("number " + randNum + "is already included in numbersList2");

			// just for checking out in console ... Debug.Log("n2 list count: " + numbersList2.Count);
			// just for checking out in console ... foreach (int num in numbersList2)  Debug.Log("+++ numbers in numberslist2: " + num);

		}
	}
}










