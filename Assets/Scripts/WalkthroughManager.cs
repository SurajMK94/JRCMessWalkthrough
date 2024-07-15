using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HotSpotsGroup
{
    public string hotSpotName;
    public string hotSpotDetails;
    public string hotSpotPosition;
    public List<TagsGroup> tagsGroup;
}

[Serializable]
public class TagsGroup
{
    public string tagName;
    public string tagDetails;
    public string tagPosition;
}

[Serializable]
public class TeleportIndicatorsGroup
{
    public string teleportIndicatorName;
    public string teleportIndicatorPosition;
}

[Serializable]
public class RoomInfo
{
    public string locationName;
    public int locationCameraAngle;
    public List<TeleportIndicatorsGroup> teleportIndicatorsGroup;
    public List<HotSpotsGroup> hotSpotsGroup;
}

public class WalkthroughManager : MonoBehaviour
{
    public TextAsset roomJson;
    public SceneDataManager sceneDataManager;
    public TeleportManager teleportManager;
    public HotSpotManager hotSpotManager;
    public TagManager tagManager;
    public RoomInfo roomInfo;

    // Start is called before the first frame update
    void Start()
    {
        sceneDataManager = GameObject.Find("SceneDataManager").GetComponent<SceneDataManager>();

        roomJson = Resources.Load("Jsons/" + sceneDataManager.roomName) as TextAsset;
        roomInfo = JsonUtility.FromJson<RoomInfo>(roomJson.text);

        teleportManager.Initialise();
        hotSpotManager.Initialise();
    }
}
