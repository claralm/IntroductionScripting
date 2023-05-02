using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    public float runSpeed;
	public float gotHayDestroyDelay;
	private bool hitByHay;
	public float dropDestroyDelay;
	
	private Collider myCollider;
	private Rigidbody myRigidbody;
	private SheepSpawner sheepSpawner;
    
    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<Collider>();
		myRigidbody = GetComponent<Rigidbody>();

    }
    
    private void Drop()
	{
		GameStateManager.Instance.DroppedSheep();
		
		sheepSpawner.RemoveSheepFromList(gameObject);
		myRigidbody.isKinematic = false;
		myCollider.isTrigger = false;
		Destroy(gameObject, dropDestroyDelay);
		SoundManager.Instance.PlaySheepDroppedClip();
	}

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
    }
    
    private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Hay") && !hitByHay)
		{
		    Destroy(other.gameObject);
		    HitByHay();
		}
		else if (other.CompareTag("DropSheep"))
		{
			Drop();
		}
	}
    
    private void HitByHay()
	{
		sheepSpawner.RemoveSheepFromList(gameObject);
		
		hitByHay = true;
		runSpeed = 0;

		Destroy(gameObject, gotHayDestroyDelay);
		SoundManager.Instance.PlaySheepHitClip();
		GameStateManager.Instance.SavedSheep();
	}
	
	public void SetSpawner(SheepSpawner spawner)
	 {
	 	sheepSpawner = spawner;
	 }
}
