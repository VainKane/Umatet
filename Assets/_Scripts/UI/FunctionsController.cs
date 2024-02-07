using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionsController : MonoBehaviour
{
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject settingButton;

    // Start is called before the first frame update
    void Start()
    {
        settingPanel.SetActive(false);
        settingButton.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UsingSettingPanel()
    {
        settingButton.SetActive(false);
        settingPanel.SetActive(true);
    }
    public void ClosingSettingPanel()
    {
        settingButton.SetActive(true);
        settingPanel.SetActive(false);
    }

}
