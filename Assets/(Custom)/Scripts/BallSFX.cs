using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSFX : MonoBehaviour
{
	public AudioSource audioSource;
	public AudioClip[] clips;
	public float volume;

	private void OnCollisionEnter(Collision collision)
	{
		audioSource.PlayOneShot(clips[0], volume);
	}

	public void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.name == "BallCatcher")
		{
			audioSource.PlayOneShot(clips[1], volume);
		}
	}
}
