using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuManager : MonoBehaviour
{
    [SerializeField] GameObject _computer;
    [SerializeField] GameObject _phone;
    [SerializeField] ComputerManager _comp;
    Animation _animation;
    [SerializeField] Button _menu, _settingsBtn, _levelSettingsBtn, _levelsBtn, _pickCompBtn,_exitBtn;
    //example button with params
    // _settingsBtn.onClick.AddListener(delegate {TaskWithParameters("Hello"); });
    
    private void Awake()
    {
        gameObject.SetActive(false);
    }
    private void Start()
    {
        //_menu.onClick.AddListener(OpenStartMenu);
        _settingsBtn.onClick.AddListener(Settings);
        _pickCompBtn.onClick.AddListener(PickUpComputer);

        // fixme inefficent 
        _animation = _phone.GetComponent<Animation>();
    }

    void OpenStartMenu()
    {
        gameObject.SetActive(true);
    }

    void Settings()
    {
        //open a new window of different settings
    }

    void LevelSettings()
    {
        // same as settings but for level stuff including 
        // restart level
        // hints
        // etc
    }

    void PickUpComputer()
    {
        _comp.ChangeComputerMode();
        gameObject.SetActive(false);
        _animation.Play("Toaster Hide");
        _computer.SetActive(false);
    }

}
