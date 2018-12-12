using UnityEngine;
using System.Collections;

public class fj : MonoBehaviour
{
    SteamVR_TrackedController trackedController;
    GameObject grababbleObject;
    FixedJoint joint;

    void Start()
    {
        trackedController = gameObject.GetComponent<SteamVR_TrackedController>();

        if (trackedController == null)
        {
            trackedController = gameObject.AddComponent<SteamVR_TrackedController>();
        }

        trackedController.TriggerClicked += new ClickedEventHandler(DoTriggerClicked);
        trackedController.TriggerUnclicked += new ClickedEventHandler(DoTriggerUnclicked);

        joint = gameObject.GetComponent<FixedJoint>();
    }

    public void DoTriggerClicked(object sender, ClickedEventArgs e)
    {
        grab();
    }

    public void DoTriggerUnclicked(object sender, ClickedEventArgs e)
    {
        release();
    }

    void grab()
    {
        if (grababbleObject == null || joint.connectedBody != null)
        {
            return;
        }

        joint.connectedBody = grababbleObject.GetComponent<Rigidbody>();
    }
    void release()
    {
        if (joint.connectedBody == null)
        {
            return;
        }

        Rigidbody rigidBody = joint.connectedBody;
        joint.connectedBody = null;

        var device = SteamVR_Controller.Input((int)trackedController.GetComponent<SteamVR_TrackedObject>().index);
        rigidBody.velocity = device.velocity;
        rigidBody.angularVelocity = device.angularVelocity;
        rigidBody.maxAngularVelocity = rigidBody.angularVelocity.magnitude;
    }

    void OnTriggerEnter(Collider other)
    {
        grababbleObject = other.gameObject;
    }

    void OnTriggerExit(Collider other)
    {
        grababbleObject = null;
    }
}