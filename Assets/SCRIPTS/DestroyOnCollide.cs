using UnityEngine;
using System.Collections;

public class DestroyOnCollide : MonoBehaviour
{

    void OnCollisionEnter2D()
    {
        Destroy(gameObject);

    }
}
