using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHandler : MonoBehaviour
{
    private LineRenderer lr;
    [SerializeField] private Transform startPoint;
    [SerializeField] private bool laserEnabled;
    [SerializeField] private int damage;
    [SerializeField] private float timeUntilNextDamageTick = .2f;
    private Vector3 laserDirection;
    // Start is called before the first frame update

    [SerializeField] private GameObject hitFeedback;
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.enabled = false;
    }

    public void ToggleLaser(bool _toggle)
    {
        laserEnabled = _toggle;
        lr.enabled = _toggle;
    }
    
    public void ToggleLaser(bool _toggle, Vector3 _direction)
    {
        laserEnabled = _toggle;
        laserDirection = _direction;
    }

    

    // Update is called once per frame
    void Update()
    {
        if(laserEnabled)
        {
            lr.enabled = true;
            lr.SetPosition(0, startPoint.position);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, /*-transform.right*/laserDirection, out hit, 99999f, ~LayerMask.GetMask("OnlyBlockPlayer")))
            {
                if (hit.collider)
                {
                    lr.SetPosition(1, hit.point);
                }
                if (hit.collider.gameObject.GetComponent<EntityHealth>() != null)
                {
                    GameObject enemyHitObj = hit.collider.gameObject;
                    EntityHealth healthobj = enemyHitObj.GetComponent<EntityHealth>();

                    if (healthobj != null)
                    {
                        healthobj.DamageEntity(1);
                    }
                }
            }
            else
                lr.SetPosition(1, -transform.right * 500000);
        }
    }
}
