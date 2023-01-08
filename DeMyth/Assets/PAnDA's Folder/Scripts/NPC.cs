using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public string npcName;
    [SerializeField] Sprite backgroundSprite;
    [SerializeField] Sprite iconSprit;

    private SpriteRenderer backgroundSpriteRenderer;
    private TextMeshPro npcNameText;
    private SpriteRenderer iconSpriteRenderer;
    
    [SerializeField] [Tooltip("The transparency must be above 0")] Color32 nameColor, backgroundColor, iconColor;

    private void Start()
    {
        // npc name adjustment
        npcNameText = transform.Find("Name").GetComponent<TextMeshPro>();

        npcNameText.SetText(npcName);
        npcNameText.color = nameColor;


        //textbackground adjustment
        backgroundSpriteRenderer = transform.Find("Background").GetComponent<SpriteRenderer>();

        backgroundSpriteRenderer.sprite = backgroundSprite;
        backgroundSpriteRenderer.color = backgroundColor;

        //npc icon adjustments
        iconSpriteRenderer = transform.Find("Icon").GetComponent<SpriteRenderer>();

        iconSpriteRenderer.sprite = iconSprit;
        iconSpriteRenderer.color = iconColor;

        // extra adjustment
        ExtraAdjustments();
    }

    private void ExtraAdjustments()
    {

        //update the text
        npcNameText.ForceMeshUpdate();

        Vector2 npcNameSize = npcNameText.GetRenderedValues(false);

        Vector2 backgroundSizeOffset = new Vector2(0f, 0.2f);


        // set the background size like the npc name 
        backgroundSpriteRenderer.size = npcNameSize + backgroundSizeOffset;

        // position the background in the right position to fit the npc name
        backgroundSpriteRenderer.transform.localPosition = new Vector3(0, npcNameText.transform.localPosition.y, 0) ;

        //position the icon in the right position
        Vector2 iconPositionOffset = new Vector2(-0.2f, 0.5f);
        iconSpriteRenderer.transform.localPosition = new Vector3( -(backgroundSpriteRenderer.size.x/2) , npcNameText.transform.localPosition.y + iconPositionOffset.y, 0);


    }
}
