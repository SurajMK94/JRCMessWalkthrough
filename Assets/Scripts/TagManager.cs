using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TagManager : MonoBehaviour
{
    public GameObject tagPrefab;
    public Transform tagParent;
    public WalkthroughManager walkthroughManager;
    public GameObject tagView;
    public Image tagImage;
    public TMPro.TextMeshProUGUI headingText;
    public TMPro.TextMeshProUGUI descriptionText;
    public List<GameObject> tags = new List<GameObject>();

    private string _positionValue;
    private string[] _splitPosition;
    public Sprite _tagSprite;

    public void Initialise(string hotSpotName)
    {
        foreach (var tag in tags)
        {
            Destroy(tag);
        }

        tags.Clear();

        for (int i = 0; i < walkthroughManager.roomInfo.hotSpotsGroup.Count; i++)
        {
            if (walkthroughManager.roomInfo.hotSpotsGroup[i].hotSpotName == hotSpotName)
            {
                for (int j = 0; j < walkthroughManager.roomInfo.hotSpotsGroup[i].tagsGroup.Count; j++)
                {
                    GameObject tagGO = Instantiate(tagPrefab, tagParent);
                    _positionValue = walkthroughManager.roomInfo.hotSpotsGroup[i].tagsGroup[j].tagPosition;
                    _splitPosition = _positionValue.Split(",");
                    tagGO.GetComponent<RectTransform>().anchoredPosition = new Vector3(float.Parse(_splitPosition[0]), float.Parse(_splitPosition[1]), float.Parse(_splitPosition[2]));

                    tagGO.name = "Tag_" + walkthroughManager.roomInfo.hotSpotsGroup[i].tagsGroup[j].tagName;
                    tagGO.GetComponent<TagData>().tagName = walkthroughManager.roomInfo.hotSpotsGroup[i].tagsGroup[j].tagName;
                    tagGO.GetComponent<TagData>().tagDescription = walkthroughManager.roomInfo.hotSpotsGroup[i].tagsGroup[j].tagDetails;

                    int hotSpotIndex = i;
                    int tagIndex = j;

                    tagGO.GetComponent<Button>().onClick.AddListener(delegate
                    {
                        ONShowTagView(walkthroughManager.roomInfo.hotSpotsGroup[hotSpotIndex].tagsGroup[tagIndex].tagName,
                            walkthroughManager.roomInfo.hotSpotsGroup[hotSpotIndex].tagsGroup[tagIndex].tagDetails);
                    });

                    tags.Add(tagGO);
                }
            }
        }
    }

    public void ONShowTagView(string tagName, string description)
    {
        tagView.SetActive(true);

        _tagSprite = Resources.Load<Sprite>("Tags/" + walkthroughManager.sceneDataManager.roomName + "/" + tagName);
        tagImage.sprite = _tagSprite;
        headingText.text = tagName;
        descriptionText.text = description;
    }
}
