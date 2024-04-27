using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    [Range(0.0f, 1.0f)]
    public float Weight; //how interested the follower is in the thing it's following :)
    [Range(-10.0f, 10.0f)]
    public float offSetZ;

    void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z + offSetZ), Weight);
    }

} 

