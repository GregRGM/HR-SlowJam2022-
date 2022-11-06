using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFiringPlatform : MonoBehaviour
{
    Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);

    [SerializeField] private LayerMask AimColliderLayerMask;
    //[SerializeField] private Transform TargetPoint;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform spawnPoint;

    private Vector3 mouseWorldPosition;

    private void Update()
    {
        mouseWorldPosition = Vector3.zero;

        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, AimColliderLayerMask))
        {
            //transform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
        }

        if (Input.GetMouseButtonDown(1))
        {
            FireShot();
        }
    }


    private void FireShot()
    {
        Vector3 AimDirection = (mouseWorldPosition - spawnPoint.position).normalized;
        Instantiate(projectilePrefab, spawnPoint.position, Quaternion.LookRotation(AimDirection, Vector3.up));
    }
}
