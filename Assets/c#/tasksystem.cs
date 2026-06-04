using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tasksystem : MonoBehaviour

{
    [Header("道具")]
    [SerializeField] private GameObject key1;
    [SerializeField] private bool haskey1 = false;
    [SerializeField] private GameObject key2;
    [SerializeField] private bool haskey2 = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

  

    // 通用的道具状态设置方法
    public void SetHasItem(string itemName, bool value)
    {
        switch (itemName)
        {
            case "bedroomkey1":
                if (!haskey1) // 确保只赋值一次
                {
                    haskey1 = value;
                    
                }
                break;
            case "bedroomkey2":
                if (!haskey2) // 确保只赋值一次
                {
                    haskey2 = value;
                    
                }
        
                break;
        }
    }
    public bool GetHasItem(string itemName)
    {
        switch (itemName)
        {
            case "bedroomkey1":
                return haskey1;
            case "bedroomkey2":
                return haskey2;
        }
        return false;
    }

    public void CompleteTask(int taskIndex)
    {
        tasklist taskList = FindObjectOfType<tasklist>();
        if (taskList != null)
        {
            taskList.CompleteTask(taskIndex);
        }
    }
}