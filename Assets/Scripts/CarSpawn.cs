using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarSpawn : MonoBehaviour 
{
	public List<Transform> cars;
	public Transform leftSpawn;
	public Transform rightSpawn;
	
	Vector3 startPoint;
	Vector3 endPoint;
	float spawnCD;
	float timer = 0f;
	bool ready = true;
	bool left;
	Transform carClone;

	void Start () 
	{
		timer = 0f;
		spawnCD = 3f;
		left = true;
	}

	void Update () 
	{
		if(ready)
		{
			ready = false;
			SpawnCar();
		}
		else if (timer > spawnCD)
		{
			ready = true;
			timer = 0f;
			spawnCD = Random.Range(2f,5f);
		}
		else 
			timer += Time.deltaTime;
	}

	void SpawnCar()
	{
		Transform car = cars[Random.Range(0, cars.Count)];

		if(left)	
		{
			startPoint = leftSpawn.position;
			endPoint = rightSpawn.position;
			left = false;
		}
		else 
		{
			startPoint = rightSpawn.position;
			endPoint = leftSpawn.position;
			left = true;
		}
		carClone = GameObject.Instantiate(car, startPoint, Quaternion.identity) as Transform;
		carClone.parent = transform;
		carClone.GetComponent<CarMovement>().DriveTo(new Vector3 (endPoint.x, startPoint.y, endPoint.z));
	}
}

