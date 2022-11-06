using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

public class PlayerFiringPlatform : MonoBehaviour
{
    [SerializeField] private LayerMask AimColliderLayerMask;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform spawnPoint;
    public Transform hitpoint;
    private Vector3 mouseWorldPosition;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private LayerMask ignoreLayer;
    

    private void Update()
    {
        mouseWorldPosition = Vector3.zero;

        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, AimColliderLayerMask))
        {
            mouseWorldPosition = raycastHit.point;
            hitpoint.position = raycastHit.point;
            //Debug.Log(mouseWorldPosition);
        }
        

        if (Input.GetMouseButtonDown(0))
        {
            //FireShot();
            FireSpread();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(spawnPoint.position, hitpoint.position);
    }
    private void FireShot()
    {
        Vector3 AimDirection = (mouseWorldPosition - spawnPoint.position).normalized;
        Instantiate(projectilePrefab, spawnPoint.position, Quaternion.LookRotation(AimDirection, Vector3.up));

        
    }
    
    private void FireSpread()
    {
        Vector3 AimDirection = (mouseWorldPosition - spawnPoint.position).normalized;
        Instantiate(projectilePrefab, spawnPoint.position, Quaternion.LookRotation(AimDirection, Vector3.up));
        Instantiate(projectilePrefab, spawnPoint.position, Quaternion.LookRotation(new Vector3(AimDirection.x * 1.1f, AimDirection.y * 1.1f, AimDirection.z), Vector3.up));
        Instantiate(projectilePrefab, spawnPoint.position, Quaternion.LookRotation(new Vector3(AimDirection.x * .9f, AimDirection.y * .9f, AimDirection.z), Vector3.up));        
    }
}
