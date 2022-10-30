using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Console;

public class StartMenuManager : MonoBehaviour, IPointerExitHandler
{
    [SerializeField] GameObject _computer;
    [SerializeField] GameObject _phone;
    [SerializeField] ComputerManager _comp;
    Animation _animation;
    [SerializeField] Button _menu, _settingsBtn, _levelSettingsBtn, _levelsBtn, _pickCompBtn, _exitBtn, _console;
    //example button with params
    // _settingsBtn.onClick.AddListener(delegate {TaskWithParameters("Hello"); });
    public void Awake()
    {
        gameObject.SetActive(false);
    }
    private void Start()
    {
        _settingsBtn.onClick.AddListener(Settings);
        _pickCompBtn.onClick.AddListener(PickUpComputer);
        _console.onClick.AddListener(CreateConsole);
        // fixme inefficent 
        _animation = _phone.GetComponent<Animation>();
    }

    public void OpenCloseStartMenu()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    void Settings()
    {
        //open a new window of different settings
        throw new System.NotImplementedException();
    }

    void LevelSettings()
    {
        // same as settings but for level stuff including 
        // restart level
        // hints
        // etc
    }

    void CreateConsole()
    {
        OpenCloseStartMenu();
        ConsoleManager.CreateConsole();
    }

    void PickUpComputer()
    {
        _comp.ChangeComputerMode(Override: true, OverrideBool: false);
        _animation.Play("Toaster Hide");
        StartCoroutine(Co_PickUpComputer());
        StartCoroutine(Co_RemoveStartMenu());
    }
    public IEnumerator Co_PickUpComputer()
    {   
        _computer.SetActive(false);
        yield return new WaitForSeconds(1);
    }
    
    public IEnumerator Co_RemoveStartMenu()
    {
        gameObject.SetActive(false);
        yield return new WaitForSeconds(2);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        gameObject.SetActive(false);
    }
}
