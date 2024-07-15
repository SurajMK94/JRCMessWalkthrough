using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotSpotManager : MonoBehaviour
{
    public GameObject hotSpotPrefab;
    public Transform hotSpotObjectParent;
    public List<Texture> scannedLocations;
    public float maxRaycastDistance = 100f;
    public WalkthroughManager walkthroughManager;
    public GameObject hotSpotView;
    public Image hotSpotImage;
    public TMPro.TextMeshProUGUI headingText;
    public TMPro.TextMeshProUGUI descriptionText;
    public List<GameObject> hotSpots = new List<GameObject>();

    private string _positionValue;
    private string[] _splitPosition;
    public Sprite _hotSpotSprite;

    public void Initialise()
    {
        foreach (var teleportPoint in hotSpots)
        {
            Destroy(teleportPoint);
        }

        hotSpots.Clear();

        for (int i = 0; i < walkthroughManager.roomInfo.hotSpotsGroup.Count; i++)
        {
            GameObject hotSpotGO = Instantiate(hotSpotPrefab, hotSpotObjectParent);
            _positionValue = walkthroughManager.roomInfo.hotSpotsGroup[i].hotSpotPosition;
            _splitPosition = _positionValue.Split(",");
            Debug.Log(float.Parse(_splitPosition[0]));
            hotSpotGO.transform.position = new Vector3(float.Parse(_splitPosition[0]), float.Parse(_splitPosition[1]), float.Parse(_splitPosition[2]));

            hotSpotGO.name = "Hotspot_" + walkthroughManager.roomInfo.hotSpotsGroup[i].hotSpotName;
            hotSpotGO.GetComponent<HotspotData>().hotSpotName = walkthroughManager.roomInfo.hotSpotsGroup[i].hotSpotName;
            hotSpotGO.GetComponent<HotspotData>().hotSpotDescription = walkthroughManager.roomInfo.hotSpotsGroup[i].hotSpotDetails;

            hotSpots.Add(hotSpotGO);
        }
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
                if (hitObject.tag == "Hotspot")
                {
                    OnShowHotSpot(hitObject.GetComponent<HotspotData>().hotSpotName,
                        hitObject.GetComponent<HotspotData>().hotSpotDescription);
                    walkthroughManager.tagManager.Initialise(hitObject.GetComponent<HotspotData>().hotSpotName);
                }
            }
        }
    }

    public void OnShowHotSpot(string hotSpotName, string description)
    {
        Camera.main.GetComponent<NavigateCamera>().enabled = false;
        hotSpotView.SetActive(true);

        _hotSpotSprite = Resources.Load<Sprite>("Hotspots/" + walkthroughManager.sceneDataManager.roomName + "/" + hotSpotName);
        hotSpotImage.sprite = _hotSpotSprite;
        headingText.text = hotSpotName;
        descriptionText.text = description;
    }

    public void OnCloseHotSpotView()
    {
        hotSpotView.SetActive(false);
        Camera.main.GetComponent<NavigateCamera>().enabled = true;
    }
}
