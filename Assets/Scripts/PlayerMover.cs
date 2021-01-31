using UnityEngine;
using System;
using PathCreation;

public class PlayerMover : MonoBehaviour
{
    public bool motor;
    public float trailHeight = 0.01f;
    [SerializeField] GameObject particles;
    float currentSpeed = 0f;
    float distanceTravelled;
    PathCreator pathCreator;

    void Awake()
    {
        pathCreator = GameObject.Find("Path").GetComponent<PathCreator>();
        transform.position = pathCreator.path.GetPointAtDistance(0);
        transform.rotation = pathCreator.path.GetRotationAtDistance(0);
        GameManager.instance.isAlive = true;
        GameManager.instance.isFinished = false;
        if (transform.GetChild(0).name == "TrailMotor")
        {
            transform.GetChild(0).GetComponent<TrailRenderer>().material = GameManager.instance.trail;
        }
    }

    void Update()
    {
        if (GameManager.instance.isAlive && (GameManager.instance.isFinished == false))
        {
            if (Input.GetMouseButtonDown(0))
            {
                motor = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                motor = false;
                if (particles != null)
                    particles.SetActive(false);
            }
            if (motor)
            {
                if (particles != null)
                    particles.SetActive(true);
                distanceTravelled += GameManager.instance.baseSpeed * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
                if (transform.GetChild(0).name == "TrailMotor")
                {
                    Vector3 newPos = transform.GetChild(0).transform.localPosition;
                    newPos.y = trailHeight;
                    transform.GetChild(0).transform.localPosition = newPos;
                }
            }
        }
    }
}
