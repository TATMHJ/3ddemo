using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class door : MonoBehaviour
{
    public float openAngle = 90f;
    public float openSpeed = 5f; // 增加速度
    public float interactionDistance = 5f; // 增加交互距离
    public string requiredKeyName; // 需要的钥匙名称

    public GameObject panel;

    private static door activeDoor;
    private static float activeDoorDistance = float.MaxValue;
    private static GameObject activePanel;

    private bool isOpen = false;
    public bool isLocked = true; // 默认锁定
    private Quaternion closedRotation;
    private Quaternion openRotation;

    public Text warningText; // 引用UI Text组件
    
    private void Start()
    {
       
        // 记录初始旋转
        closedRotation = transform.rotation;
        // 计算打开时的旋转（绕Y轴旋转）
        openRotation = closedRotation * Quaternion.Euler(0f, openAngle, 0f);

        if (string.IsNullOrEmpty(requiredKeyName)&&isLocked)
        {
            Debug.LogWarning("Door '" + gameObject.name + "' is missing a requiredKeyName. Please set it in the inspector.");
        }

        if (panel != null && activePanel == null)
        {
            activePanel = panel;
        }
        
        
    }
    
    private void Update()
    {
        // 检查玩家距离
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance <= interactionDistance && (activeDoor == null || activeDoor == this || distance < activeDoorDistance))
            {
                SetActiveDoor(distance);
            }
            else if (activeDoor == this && distance > interactionDistance)
            {
                ClearActiveDoor();
            }

            // 只有当前激活的门可以控制面板和交互
            if (activeDoor == this && activePanel != null)
            {
                activePanel.SetActive(true);
            }
            
            // 当玩家靠近时按F键切换门状态
            if (activeDoor == this && Input.GetKeyDown(KeyCode.F))
            {
                if (!isLocked)
                {
                    isOpen = !isOpen;
                    Debug.Log("Door state changed: " + (isOpen ? "Open" : "Closed"));
                }
                else
                {
                    // 检查玩家是否有钥匙
                    tasksystem taskSystem = FindObjectOfType<tasksystem>();
                    if (taskSystem != null && taskSystem.GetHasItem(requiredKeyName))
                    {
                        isLocked = false;
                        isOpen = true;
                        Debug.Log("Door unlocked and opened with key: " + requiredKeyName);
                        if (requiredKeyName == "bedroomkey1")
                        {
                            taskSystem.CompleteTask(0);
                        }
                    }
                    else
                    {
                        Debug.Log("Door is locked. Required key: " + requiredKeyName);
                        StartCoroutine(ShowWarning());
                    }
                }
            }
        }
        else
        {
            Debug.LogWarning("Player not found! Make sure your player has the 'Player' tag.");
        }
        
        // 平滑旋转门
        Quaternion targetRotation = isOpen ? openRotation : closedRotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * openSpeed);
    }

    IEnumerator ShowWarning()
    {
        if (warningText != null)
        {
            warningText.text = "需要 " + requiredKeyName;
            warningText.gameObject.SetActive(true);
            yield return new WaitForSeconds(2f); // 显示2秒
            warningText.gameObject.SetActive(false);
        }
    }

    private void SetActiveDoor(float distance)
    {
        if (activeDoor != this && activePanel != null && activePanel != panel)
        {
            activePanel.SetActive(false);
        }

        activeDoor = this;
        activeDoorDistance = distance;
        activePanel = panel;

        if (activePanel != null)
        {
            activePanel.SetActive(true);
        }
    }

    private void ClearActiveDoor()
    {
        if (activeDoor == this)
        {
            if (activePanel != null)
            {
                activePanel.SetActive(false);
            }

            activeDoor = null;
            activeDoorDistance = float.MaxValue;
        }
    }
}