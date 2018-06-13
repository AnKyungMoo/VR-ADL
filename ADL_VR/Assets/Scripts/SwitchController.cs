using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    [SerializeField] private GameObject _light1 = null;
    [SerializeField] private GameObject _light2 = null;

    public void TouchSwitch()
    {
        _light1.GetComponent<Light>().enabled = false;
        _light2.GetComponent<Light>().enabled = false;
    }
}
