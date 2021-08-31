using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class HMDInfoManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Device Active " + XRSettings.isDeviceActive);
        //Debug.Log("Device Name " + XRSettings.loadedDeviceName);

        if (!XRSettings.isDeviceActive)
        {

            Debug.Log("NO Headset");
        }
        else if (XRSettings.isDeviceActive && (XRSettings.loadedDeviceName == "MockHMD Display" || XRSettings.loadedDeviceName == "Mock HMD"))
        {
            Debug.Log("Using MockHMD Display");
        }
        else {

            Debug.Log("Device Being Used:  " + XRSettings.loadedDeviceName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
