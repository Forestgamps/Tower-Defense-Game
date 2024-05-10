using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : NetworkBehaviour
{
    public Color hoverColor;

    public GameObject prefab;

    private GameObject turret;

    private Renderer rend;
    private Color startColor;
    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    void Update()
    {

    }

    private void OnMouseDown()
    {
        if (turret != null)
        {
            Debug.Log("Can't build here");
            return;
        }
        
        //CmdSpawnTurret();
        //turret.transform.position = transform.position;
    }

    [Server]
    void SpawnTurret()
    {
        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, transform.position, transform.rotation);
        NetworkServer.Spawn(turret);
    }

    [Command]
    public void CmdSpawnTurret()
    {
        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, transform.position, transform.rotation);
        NetworkServer.Spawn(turret);
    }

    private void OnMouseEnter()
    {
        rend.material.color = hoverColor;
    }
    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
