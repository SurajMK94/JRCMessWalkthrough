using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    public List<Texture> scannedLocations;
    public GameObject telePortPrefab;
    public Transform teleportObjectParent;
    public Material sceneMaterial;
    public float maxRaycastDistance = 100f;
    public WalkthroughManager walkthroughManager;
    public List<GameObject> teleportPoints = new List<GameObject>();
    public TMPro.TextMeshProUGUI roomName;

    private Texture _locationTexture;
    private string _positionValue;
    private string[] _splitPosition;

    public void Initialise()
    {
        foreach (var teleportPoint in teleportPoints)
        {
            Destroy(teleportPoint);
        }

        teleportPoints.Clear();

        _locationTexture = Resources.Load("360Images/" + walkthroughManager.sceneDataManager.roomName) as Texture;
        sceneMaterial.SetTexture("_MainTex", _locationTexture);
        Camera.main.transform.eulerAngles = new Vector3(0, walkthroughManager.roomInfo.locationCameraAngle, 0);
        Camera.main.GetComponent<NavigateCamera>().Initialise();

        for (int i = 0; i < walkthroughManager.roomInfo.teleportIndicatorsGroup.Count; i++)
        {
            GameObject telePortGO = Instantiate(telePortPrefab, teleportObjectParent);
            _positionValue = walkthroughManager.roomInfo.teleportIndicatorsGroup[i].teleportIndicatorPosition;
            _splitPosition = _positionValue.Split(",");
            Debug.Log(float.Parse(_splitPosition[0]));
            telePortGO.transform.position = new Vector3(float.Parse(_splitPosition[0]), float.Parse(_splitPosition[1]), float.Parse(_splitPosition[2]));
            
            telePortGO.name = "TeleportPoint_" + walkthroughManager.roomInfo.teleportIndicatorsGroup[i].teleportIndicatorName;
            telePortGO.GetComponent<TeleportData>().roomName = walkthroughManager.roomInfo.teleportIndicatorsGroup[i].teleportIndicatorName;

            teleportPoints.Add(telePortGO);
        }

        roomName.text = walkthroughManager.sceneDataManager.roomName;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, maxRaycastDistance))
            {
                // Get the game object that was hit by the raycast
                GameObject hitObject = hit.transform.gameObject;

                // Do something with the hit object
                Debug.Log("Clicked on: " + hitObject.name);
                if (hitObject.tag == "Teleport")
                { 
                    OnSwitchView(hitObject.GetComponent<TeleportData>().roomName);
                }
            }
        }
    }

    public void OnSwitchView(string roomName)
    {
        walkthroughManager.sceneDataManager.roomName = roomName;
        walkthroughManager.roomJson = Resources.Load("Jsons/" + walkthroughManager.sceneDataManager.roomName) as TextAsset;
        walkthroughManager.roomInfo = JsonUtility.FromJson<RoomInfo>(walkthroughManager.roomJson.text);

        Initialise();

        walkthroughManager.hotSpotManager.Initialise();
    }
}
