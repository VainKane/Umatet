using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject settingPanel;

    // Start is called before the first frame update
    void Start()
    {
        settingPanel.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void WhenUsingSettingMenu()
    {
        settingPanel.SetActive(true);
    }
}
