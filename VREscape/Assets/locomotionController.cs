using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class locomotionController : MonoBehaviour
{
    // Start is called before the first frame update

    public XRController LeftTeleportRay;
    public XRBaseController RigthTeleportRay;
    public InputHelpers.Button teleportActivationButton;
    public float activationThreshold = .1f;

    // Update is called once per frame
    public void Update()
    {
        if(LeftTeleportRay)
        {LeftTeleportRay.gameObject.SetActive(CheckIfActivated(LeftTeleportRay)); }
    }

    public bool CheckIfActivated(XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, teleportActivationButton, out bool isActivated, activationThreshold);
        return isActivated;


    }
}
