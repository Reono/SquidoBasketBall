using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
	public SphereCollider pickupCollider;
	public GameObject basketball;
	public GameObject playerPosition;
	public bool hasBall = false;
	public float forwardPower;
	public float heightPower;
	public Slider powerBar;

	private void Start()
	{
		if(!pickupCollider)
		{
			pickupCollider = GetComponent<SphereCollider>();
		}

		if(!playerPosition)
		{
			playerPosition = transform.parent.gameObject;
		}
	}

	private void Update()
	{
		//Once player holds down left click pingpong throw power
		if (Input.GetMouseButton(0) && hasBall)
		{
			forwardPower = Mathf.PingPong(Time.time * 10, 10);
		}

		//Once left click is released throw ball
		if (Input.GetMouseButtonUp(0) && hasBall)
		{
			//Turn off pickup collider
			pickupCollider.enabled = false;

			//Player no longer has the ball
			hasBall = false;

			//Add gravity back
			basketball.GetComponent<Rigidbody>().useGravity = true;

			//Add forward and up forces to the ball
			basketball.GetComponent<Rigidbody>().AddForce(playerPosition.transform.forward * forwardPower, ForceMode.Impulse);
			basketball.GetComponent<Rigidbody>().AddForce(playerPosition.transform.up * heightPower, ForceMode.Impulse);

			//Reset pickup collider
			StartCoroutine(ResetCollider());
		}

		UpdatePowerBar();
	}

	private void UpdatePowerBar()
	{
		//Update value of slider with current forward power
		powerBar.value = forwardPower;
		
		//Heightpower will always be 1 less then forward power
		heightPower = forwardPower - 1;
	}

	private void OnTriggerStay(Collider other)
	{
		if(other.gameObject.name == "Basketball")
		{
			//Change balls position
			other.transform.position = this.transform.position;

			//Get basket ball object
			basketball = other.gameObject;

			//Make sure to set bool to true
			hasBall = true;

			//Zero out any rotation on ball
			basketball.transform.rotation = Quaternion.identity;

			//Turn off gravity
			basketball.GetComponent<Rigidbody>().useGravity = false;
		}
	}

	IEnumerator ResetCollider()
	{
		//wait for a second before turning on collider
		yield return new WaitForSeconds(1f);
		pickupCollider.enabled = true;
	}
}
