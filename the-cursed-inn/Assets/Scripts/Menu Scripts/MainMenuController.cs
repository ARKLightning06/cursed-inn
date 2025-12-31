using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartGameOnClick()
    {

        SceneManager.LoadScene("Replace with the actual start game scene");

    }
    public void ExitGameOnClick()
    {

        Application.Quit();

    }

    //probably load all the settings from a json/txt file once this action is performed. 

    public void SettingsOnClick()
    {

        SceneManager.LoadScene("Replace with the actual settings game scene");

    }

}
