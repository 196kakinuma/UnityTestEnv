using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCtrl : MonoBehaviour {


    GameObject manipulateObj = null;
    bool isManipulating = false;
    Vector3 startDirection = Vector3.zero;
    GameObject parentObj;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update()
    {

        var trackedObject = GetComponent<SteamVR_TrackedObject>();
        var device = SteamVR_Controller.Input((int)trackedObject.index);

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            if(manipulateObj!=null)
            {
                parentObj = new GameObject();
                parentObj.transform.position = manipulateObj.transform.position;
                parentObj.transform.LookAt(this.transform.position);
                manipulateObj.transform.parent = parentObj.transform;
            }
        }
        if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
            //Debug.Log("トリガーを深く引いている");
			if (manipulateObj != null && parentObj!=null)
            {
                isManipulating = true;
                //移動した分だけ回転させる
                parentObj.transform.LookAt(this.transform.position);

            }
        }
        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
			if (manipulateObj != null) {
				manipulateObj.transform.parent = null;
				var a = parentObj;
				parentObj = null;
				Destroy (a);
			}

            isManipulating = false;
            manipulateObj = null;

        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (manipulateObj != null) return;

        if (other.gameObject.tag == "RotateObject")
        {
            manipulateObj = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isManipulating) return;
        if (other.gameObject.tag != "RotateObject") return;
        manipulateObj = null;

    }
}
