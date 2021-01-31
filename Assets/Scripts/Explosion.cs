using UnityEngine;

public class Explosion : MonoBehaviour
{
    public Material pinkyMaterial;

    public float cubeSize = 0.2f;
    public int cubesInRow = 5;

    float m_cubesPivotDistance;
    Vector3 m_cubesPivot;

    public float explosionForce = 50f;
    public float explosionRadius = 4f;
    public float explosionUpward = 0.4f;

    void Start()
    {
        m_cubesPivotDistance = cubeSize * cubesInRow / 2;
        m_cubesPivot = new Vector3(m_cubesPivotDistance, m_cubesPivotDistance, m_cubesPivotDistance);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Explode();
            GameManager.Die();
        }
        if (other.gameObject.tag == "Finish")
        {
            GameManager.Finish();
            Explode();
        }
    }

    public void Explode()
    {
        GameManager.instance.isAlive = false;
        gameObject.SetActive(false);
        for (int x = 0; x < cubesInRow; x++)
        {
            for (int y = 0; y < cubesInRow; y++)
            {
                for (int z = 0; z < cubesInRow; z++)
                {
                    CreatePiece(x, y, z);
                }
            }
        }
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpward);
            }
        }
    }

    void CreatePiece(int x, int y, int z)
    {
        GameObject piece;
        piece = GameObject.CreatePrimitive(PrimitiveType.Cube);
        piece.GetComponent<MeshRenderer>().material = pinkyMaterial;
        piece.transform.position = transform.position + new Vector3(cubeSize * x, cubeSize * y, cubeSize * z) - m_cubesPivot;
        piece.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);
        piece.AddComponent<Rigidbody>();
        piece.GetComponent<Rigidbody>().mass = cubeSize;
        //piece.AddComponent<AutoDestruction>();
    }

}