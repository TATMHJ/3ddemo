using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tasklist : MonoBehaviour
{
    [SerializeField]private GameObject taskPanel; // 任务面板
  
    [Header("任务列表")]    
    [SerializeField] private List<Text> tasks = new List<Text>();
    [SerializeField] private bool[] taskfinished = new bool[0];
    // Start is called before the first frame update
    void Start()
    {
        taskPanel.SetActive(false);
        tasks[0].gameObject.SetActive(true);
        for (int i = 1; i < tasks.Count; i++)
        {
            tasks[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            taskPanel.SetActive(true);
        }
        else
        {
            taskPanel.SetActive(false);
        }
       for (int i = 0; i < tasks.Count; i++)
       {
           if(taskfinished[i])
           {
               tasks[i+1].gameObject.SetActive(true);
           }
       }
    }

    public void CompleteTask(int taskIndex)
    {
        if (taskIndex >= 0 && taskIndex < taskfinished.Length)
        {
            taskfinished[taskIndex] = true;
        }
    }
}
