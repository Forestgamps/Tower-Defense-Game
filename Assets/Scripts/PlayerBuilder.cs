using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerBuilder : NetworkBehaviour
{
    public GameObject buildingPrefab; // ������ ������ ��� ������
    public LayerMask buildableLayer; // ����, ����� ������� ������ ��������� ��� ��������
    public string buildableTag; // ��� ��������, ����� ������� ������ ��������� ��� ��������

    private GameObject turret;

    void Update()
    {
        if (!isLocalPlayer) return;

        // ��������� ������� ������ ����
        if (Input.GetMouseButtonDown(0)) // 0 ������������� ����� ������ ����
        {
            // �������� ������� ������� � ������� �����������
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            // ���������, �������� �� ��� �������� ����� ������ ������������� ���� ��� � ������������ �����
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, buildableLayer))
            {
                GameObject hitObject = hitInfo.collider.gameObject;

                CmdSpawnTurret(hitObject);
            }
        }
    }

    [Command]
    public void CmdSpawnTurret(GameObject node)
    {
        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, node.transform.position, node.transform.rotation);
        NetworkServer.Spawn(turret);
    }
}
