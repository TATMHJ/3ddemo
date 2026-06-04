using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class obtainitem : MonoBehaviour
{
    [Header("道具设置")]
    [SerializeField] private string itemName; // 道具名称，如 "key1"
    [SerializeField] private GameObject catchpanel;

  

    [Header("交互设置")]
    [SerializeField] private float interactionAngle = 15f; // 物体需要在摄像机中心视角左右多少度内才能交互

    private bool isclose = false;
    private bool iscatch = false;
    void Start()
    {
        catchpanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isclose && IsObjectInCenterOfView())
        {
            catchpanel.SetActive(true);
      
            // 检查物体是否在摄像机中心
            if (Input.GetKeyDown(KeyCode.F))
            {
                catchpanel.SetActive(false);
                iscatch = true;

                // 通知tasksystem更新道具状态
                tasksystem taskSystem = FindObjectOfType<tasksystem>();
                if (taskSystem != null)
                {
                    taskSystem.SetHasItem(itemName, true);
                }

                Destroy(gameObject);
            }
        }
        else
        {
            catchpanel.SetActive(false);
        }
        
    }

    private bool IsObjectInCenterOfView()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found!");
            return false;
        }

        // 获取从摄像机到该物体的方向向量
        Vector3 directionToObject = (transform.position - mainCamera.transform.position).normalized;

        // 计算该方向向量与摄像机正前方向量之间的夹角
        float angle = Vector3.Angle(mainCamera.transform.forward, directionToObject);

        // 如果夹角小于设定的交互角度，则认为物体在中心
        return angle <= interactionAngle;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player"&& !iscatch)
        {
           
            isclose = true;
        }
    }   
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player"&& !iscatch)
        {
            
            isclose = false;
        }
    }
}
