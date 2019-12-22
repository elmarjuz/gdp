using UnityEngine;
using System.Collections;

public class destroyIfOutOfFrame : MonoBehaviour
{

    void OnBecameInvisible()
    {
        Destroy(transform.parent.gameObject);
    }

}
