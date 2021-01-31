using UnityEngine;

public class EnemyRotator : MonoBehaviour
{
    public Axis rotationAxis;
    public float speed = 5;

    void Update()
    {
        switch (rotationAxis)
        {
            case Axis.X:
                transform.Rotate(new Vector3(1f, 0f, 0f), speed);
                break;
            case Axis.Y:
                transform.Rotate(new Vector3(0f, 1f, 0f), speed);
                break;
            case Axis.Z:
                transform.Rotate(new Vector3(0f, 0f, 1f), speed);
                break;
        }
    }
}

public enum Axis { X, Y, Z }