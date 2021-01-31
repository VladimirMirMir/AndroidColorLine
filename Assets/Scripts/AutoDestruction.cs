using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestruction : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(StartProcess());
    }

    public IEnumerator StartProcess()
    {
        yield return new WaitForSeconds(1f);
        yield return GetSmaller();
    }

    public IEnumerator GetSmaller()
    {
        if (transform.localScale.x >= 0.01f)
        {
            transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
            yield return new WaitForSeconds(0.1f);
            yield return GetSmaller();
        }
        else
            yield return Destr();
    }

    public IEnumerator Destr()
    {
        yield return null;
        Destroy(gameObject);
    }
}
