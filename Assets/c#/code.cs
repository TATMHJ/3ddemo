using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SojaExiles; // 1. 添加这行来引入 opencloseDoor 脚本所在的命名空间

public class code : MonoBehaviour
{
    [SerializeField] private GameObject codePanel;
    [SerializeField] private GameObject door; // 门
    [SerializeField] private Text codeText; //提示文本
    private opencloseDoor doorScript; // 2. 创建一个变量来缓存组件，提高性能
    private bool isclose = false;
  
    // Start is called before the first frame update
    void Start()
    {
        codePanel.SetActive(false);
        codeText.gameObject.SetActive(false);
        // 3. 在开始时获取一次组件并存起来，避免每次都调用GetComponent
        if (door != null)
        {
            doorScript = door.GetComponent<opencloseDoor>();
        }
    }

    // Update is called once per frame
    void Update()
    {
       if (isclose&&Input.GetKeyDown(KeyCode.F))
       {
          codePanel.SetActive(!codePanel.activeSelf); // 切换面板显示状态

       }
       if(!isclose)
      {
        codePanel.SetActive(false);
      }
    }
    void OnTriggerStay(Collider other)
    {
        // 4. 使用缓存的变量来访问 'open' 属性
        if (other.CompareTag("Player") && doorScript != null && doorScript.open == true)
        {
            isclose = true;
            // 在这里可以根据需要激活 codePanel
            codeText.gameObject.SetActive(true);
            
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isclose = false;
            codeText.gameObject.SetActive(false);
            
            
        }
    }
}
