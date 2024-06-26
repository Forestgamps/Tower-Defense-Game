using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More Than One BuildManager in scene!!!");
            return;
        }
        instance = this;
    }

    public GameObject standartTurretPrefab;

    private void Start()
    {
        turretToBuild = standartTurretPrefab;
    }

    private GameObject turretToBuild;

    public GameObject GetTurretToBuild() 
    {
        return turretToBuild;
    }
}
