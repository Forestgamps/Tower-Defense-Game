using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerBuilder : NetworkBehaviour
{
    public GameObject buildingPrefab; // Префаб здания для спавна
    public LayerMask buildableLayer; // Слой, через который должен проходить луч рейкаста
    public string buildableTag; // Тег объектов, через которые должен проходить луч рейкаста

    private GameObject turret;

    void Update()
    {
        if (!isLocalPlayer) return;

        // Проверяем нажатие кнопки мыши
        if (Input.GetMouseButtonDown(0)) // 0 соответствует левой кнопке мыши
        {
            // Получаем позицию курсора в мировых координатах
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            // Проверяем, проходит ли луч рейкаста через объект определенного слоя или с определенным тегом
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
