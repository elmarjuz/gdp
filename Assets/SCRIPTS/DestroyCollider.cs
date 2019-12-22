using UnityEngine;
using System.Collections;

public class DestroyCollider : MonoBehaviour
{



    void OnTriggerEnter2D(Collider2D other)
    {
        print(other.gameObject.name);
        Destroy(gameObject);

    }


}
