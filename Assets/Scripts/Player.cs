using Mirror;
using Mirror.Examples.AdditiveLevels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public Camera mainCamera;

    public float moveSpeed;

    public float scrollSpeed = 5f; // Скорость перемещения камеры
    public float scrollEdge = 0.01f; // Расстояние от края экрана, при котором начинается перемещение

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer) return;
        float hor = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");
        transform.Translate(new Vector3(hor, 0, vert) * moveSpeed, Space.World);
        CameraMovement();
        //ScrollCam();
    }

    private void CameraMovement()
    {
        if(mainCamera != null)
        {
            mainCamera.transform.position = new Vector3(transform.position.x, 8f, transform.position.z);
        }
        
    }

    void ScrollCam()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 moveDirection = Vector3.zero;

        // Проверяем, находится ли курсор у края экрана
        if (mousePosition.x < Screen.width * scrollEdge)
        {
            moveDirection += Vector3.left;
        }
        else if (mousePosition.x > Screen.width * (1 - scrollEdge))
        {
            moveDirection += Vector3.right;
        }

        if (mousePosition.y < Screen.height * scrollEdge)
        {
            moveDirection += Vector3.back;
        }
        else if (mousePosition.y > Screen.height * (1 - scrollEdge))
        {
            moveDirection += Vector3.forward;
        }

        // Нормализуем вектор направления, чтобы скорость была одинаковой при движении по диагонали
        moveDirection.Normalize();

        // Перемещаем камеру
        transform.Translate(moveDirection * scrollSpeed * Time.deltaTime, Space.World);
    }
}
