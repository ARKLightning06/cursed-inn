using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerOfScenes : MonoBehaviour
{
    public void StartGameOnClick()
    {

        SceneManager.LoadScene("Inn Interior");

    }
    public void ExitGameOnClick()
    {

        Application.Quit();

    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //probably load all the settings from a json/txt file once this action is performed. 

    public void SettingsOnClick()
    {

        SceneManager.LoadScene("Replace with the actual settings game scene");

    }

}