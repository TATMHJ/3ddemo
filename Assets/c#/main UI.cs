using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainUI : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    // Start is called before the first frame update
    void Start()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
    }

    // Update is called once per frame
    public void entergame()
    {
        SceneManager.LoadScene("1");
    }
    public void exitgame()
    {
        Application.Quit();
    }
    public void opensettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true);
        }
    }
    public void closesettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
    }
}
