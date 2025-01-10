using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountScore : MonoBehaviour
{
	public TextMeshProUGUI scoreText;
	public bool scoringCooldown = false;
	public int score;

	public void Update()
	{
		UpdateScore();
	}

	private void UpdateScore()
	{
		//Update text with int score
		scoreText.text = score.ToString();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "Basketball" && !scoringCooldown)
		{
			//Set to true so player can't score more then once
			scoringCooldown = true;

			//Add one to current score
			score++;

			//Start scoring cooldown
			StartCoroutine(StartCooldown());
		}
	}

	IEnumerator StartCooldown()
	{
		//wait for a second before setting cooldown back to false
		yield return new WaitForSeconds(2f);
		scoringCooldown = false;
	}
}
