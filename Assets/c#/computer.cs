using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class computer : MonoBehaviour
{
    [SerializeField]private GameObject computerPanel;
    [SerializeField]private Text opencomputer;
    [SerializeField]private InputField passwordField; // 密码输入框
    [SerializeField]private Button submitButton; // 提交按钮
    [SerializeField]private string correctPassword = "3221"; // 正确的密码
    [SerializeField]private GameObject nextStepPanel; // 下一步的面板
    private bool isclose = false;
    // Start is called before the first frame update
    void Start()
    {
        computerPanel.SetActive(false);
        opencomputer.gameObject.SetActive(false);
        if (nextStepPanel != null)
        {
            nextStepPanel.SetActive(false);
        }
        // 添加按钮点击事件
        if (submitButton != null)
        {
            submitButton.onClick.AddListener(CheckPassword);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && isclose)
        {
            bool isPanelActive = !computerPanel.activeSelf;
            computerPanel.SetActive(isPanelActive);

            if (isPanelActive)
            {
                // 解锁并显示鼠标
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                // 锁定并隐藏鼠标
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
      
    }

    public void CheckPassword()
    {
        if (passwordField.text == correctPassword)
        {
            Debug.Log("密码正确!");
            // 隐藏当前面板，显示下一步面板
            computerPanel.SetActive(false);
            if (nextStepPanel != null)
            {
                nextStepPanel.SetActive(true);
            }
        }
        else
        {
            Debug.Log("密码错误!");
            // 清空输入框
            passwordField.text = "";
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           
            opencomputer.gameObject.SetActive(true);
            isclose = true;
            // 在这里可以添加打开电脑界面的代码
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            opencomputer.gameObject.SetActive(false);
            isclose = false;
            computerPanel.SetActive(false);
            if (nextStepPanel != null)
            {
                nextStepPanel.SetActive(false);
            }
            // 在这里可以添加关闭电脑界面的代码
        }
    }
}
