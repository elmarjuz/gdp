using UnityEngine;
using System.Collections;

public class hilariousWaitAndDestroyOnCollide : MonoBehaviour
{

    void OnCollisionEnter2D()
    {
        StartCoroutine(Wait());


    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
