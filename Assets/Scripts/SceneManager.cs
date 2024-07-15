using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    private SceneDataManager _sceneDataManager;

    private void Start()
    {
        _sceneDataManager = GameObject.Find("SceneDataManager").GetComponent<SceneDataManager>();
    }

    /// <summary>
    /// Loads the Module Selection scene
    /// </summary>
    /// <param name="index">Scene index</param>
    public void OnHomeButtonClick(int index)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
    }

    /// <summary>
    /// Loads the System Selection scene
    /// </summary>
    /// <param name="index">Scene index</param>
    public void OnSceneChange(int index)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
    }

    public void OnRoomSelected(string roomName)
    {
        _sceneDataManager.roomName = roomName;

        //if (roomName == "SilverGallery")
        //{
        //    UnityEngine.SceneManagement.SceneManager.LoadScene(3);
        //}
        //else
        //{
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        //}
    }

    /// <summary>
    /// Exit from the Application
    /// </summary>
    public void OnExitButtonClick()
    {
        Application.Quit();
    }
}
