using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Main : NetworkBehaviour
{
    public GameObject Cube;

    void Start ()
    {
    }

    void Update ()
    {
        if ( Time.frameCount % 300 == 0 )
        {
            CmdCreateCube ();
        }
    }

    [Command]
    void CmdCreateCube ()
    {
        GameObject obj = ( GameObject ) Instantiate (Cube, transform.position, transform.rotation);
        NetworkServer.Spawn (obj);
    }
}