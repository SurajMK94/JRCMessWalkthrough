using UnityEngine;

public class LoginManager : MonoBehaviour
{
    public TMPro.TMP_InputField userNameInputField;
    public TMPro.TMP_InputField passwordInputField;
    public SceneManager sceneManager;

    public void OnLoginClick()
    {
        if (userNameInputField.text == "JRC" && passwordInputField.text == "JRC@123")
        {
            sceneManager.OnSceneChange(1);
        }
        else
        {
            Debug.Log("Wrong Username or Password");
        }
    }
}
