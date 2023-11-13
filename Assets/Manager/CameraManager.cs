using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject target;
    public float moveSpeed;
    private Vector3 targetPosition;

    public Knight knight;

    private void Start()
    {
        targetPosition.Set(target.transform.position.x, target.transform.position.y + 3, this.transform.position.z);
    }
    void Update()
    {
        if (target.gameObject != null)
        {
            targetPosition.Set(target.transform.position.x, gameObject.transform.position.y, this.transform.position.z);

            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
}
