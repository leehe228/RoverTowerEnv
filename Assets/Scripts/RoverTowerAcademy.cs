using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;

public class RoverTowerAcademy : MonoBehaviour
{
    public void Awake() {
        Academy.Instance.OnEnvironmentReset += EnvironmentReset;
    }

    public void EnvironmentReset() {

    }
}