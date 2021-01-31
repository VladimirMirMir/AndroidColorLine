using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class TrailMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    float distanceTravelled;
    PathCreator pathCreator;
	PlayerMover mover;

	void Awake()
	{
		pathCreator = GameObject.Find("Path").GetComponent<PathCreator>();
		mover = GameObject.Find("Motor").GetComponent<PlayerMover>();
		transform.position = pathCreator.path.GetPointAtDistance(0);
	}
	
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            mover.motor = true;
        }
        if(Input.GetMouseButtonUp(0))
        {
            mover.motor = false;
        }
        if(mover.motor)
        {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
            Quaternion temp = pathCreator.path.GetRotationAtDistance(distanceTravelled);
            transform.localEulerAngles = new Vector3(90f, 90f, temp.eulerAngles.z);
            transform.localPosition += new Vector3(0f, 0.01f, 0f);
        }

    }
}
