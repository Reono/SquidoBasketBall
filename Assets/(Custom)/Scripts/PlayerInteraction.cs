using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
	public SphereCollider pickupCollider;
	public GameObject basketball;
	public GameObject playerPosition;
	public bool hasBall = false;
	public float forwardPower;
	public float heightPower;

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
		//Once player has ball and left mouse is clicked
		if (Input.GetMouseButtonDown(0) && hasBall)
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
