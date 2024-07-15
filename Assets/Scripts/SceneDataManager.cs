using UnityEngine;

public class SceneDataManager : MonoBehaviour
{
    #region Public Variables
    public string roomName;
    #endregion

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}