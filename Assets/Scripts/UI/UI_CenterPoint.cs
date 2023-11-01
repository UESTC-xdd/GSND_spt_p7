using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CenterPoint : MonoBehaviour
{
    public GameObject HandObj;

    public void SetHandEnabled(bool enabled)
    {
        HandObj.SetActive(enabled);
    }
}
