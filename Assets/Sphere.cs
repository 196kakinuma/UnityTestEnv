using UnityEngine;
using System.Collections;

public class Sphere : MonoBehaviour
{
    void Start ()
    {
        Destroy (gameObject, 5f);
    }

    void Update ()
    {
        transform.position += transform.forward * 2.0f;
    }
}