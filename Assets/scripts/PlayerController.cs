using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public PowerUpType currentPowerUp = PowerUpType.None;
	public GameObject rocketPrefab;
	private GameObject tmpRocket;
	private Coroutine powerupCountdown;
	private Rigidbody playerRB;
	private GameObject focalPoint;
	public float speed = 5.0f;
	public bool hasPowerup;
	public GameObject powerupIndicator;
	private float powerupStrength=15.0f;

	// Start is called before the first frame update
	void Start()
	{
		playerRB = GetComponent<Rigidbody>();
		focalPoint=GameObject.Find("Focal Point");
	}

	// Update is called once per frame
	void Update()
	{
		float forwardInput = Input.GetAxis("Vertical");
		playerRB.AddForce(focalPoint.transform.forward * speed * forwardInput);
		powerupIndicator.transform.position=transform.position +new Vector3(0,-0.5f,0);
		if (currentPowerUp == PowerUpType.Rockets && Input.GetKeyDown(KeyCode.F))
		{
			LaunchRockets();
		}

		
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Powerup"))
		{
			hasPowerup=true;
			currentPowerUp = other.gameObject.GetComponent<PowerUp>().powerUpType;
			powerupIndicator.gameObject.SetActive(true);
			Destroy(other.gameObject);
			
			if(powerupCountdown != null)
			{
				StopCoroutine(powerupCountdown);
			}
			powerupCountdown = StartCoroutine(PowerupCountdownRoutine());
			
			
			
			
		}
		
		
	}
	
	IEnumerator PowerupCountdownRoutine()
	{
		yield return new WaitForSeconds(7);
		hasPowerup=false;
		currentPowerUp = PowerUpType.None;

		powerupIndicator.gameObject.SetActive(false);
		
		
		
	}
	
	
	
	
	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.CompareTag("Enemy") && currentPowerUp ==
		   PowerUpType.Pushback)
		{
			Rigidbody enemyRigidibody= collision.gameObject.GetComponent<Rigidbody>();
			Vector3 awayFromPlayer = (collision.gameObject.transform.position -transform.position);
			Debug.Log("Collide with" + collision.gameObject.name +"with powerup set to " + hasPowerup);
			enemyRigidibody.AddForce(awayFromPlayer * powerupStrength,ForceMode.Impulse);
			Debug.Log("Player collided with: " + collision.gameObject.name + " withpowerup set to " + currentPowerUp.ToString());
			
		}
		
		void LaunchRockets()
		{
			foreach(var enemy in FindObjectsOfType<Enemy>())
			{
				tmpRocket = Instantiate(rocketPrefab, transform.position + Vector3.up,
				                        Quaternion.identity);
				tmpRocket.GetComponent<RocketBehaviour>().Fire(enemy.transform);
			}
		}

		
	}
}