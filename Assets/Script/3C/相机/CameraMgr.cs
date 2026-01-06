using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMgr : MonoBehaviour
{
    [SerializeField] GameObject PlanetCamera;
    [SerializeField] GameObject SpaceCamera;

    void OnEnable()
    {
        LandAndLauncher.OnLandPlanet += CheckToPlanetCamera;
        BaseCraft.OnLaunchOut += CheckToSpaceCamera;
    }

    void OnDisable()
    {
        LandAndLauncher.OnLandPlanet -= CheckToPlanetCamera;
        BaseCraft.OnLaunchOut -= CheckToSpaceCamera;
    }

    void CheckToPlanetCamera(Vector3 pos)
    {
        SpaceCamera.SetActive(false);
        PlanetCamera.SetActive(true);
    }
    void CheckToSpaceCamera()
    {
        SpaceCamera.SetActive(true);
        PlanetCamera.SetActive(false);
    }
}
