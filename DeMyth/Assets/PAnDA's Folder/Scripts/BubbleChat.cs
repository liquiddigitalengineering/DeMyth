using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BubbleChat : MonoBehaviour
{
    private TextMeshPro bubbleText;
    private SpriteRenderer backgroundSpriteRenderer;
    private SpriteRenderer iconSpritRenderer;


    [SerializeField] Sprite playerIconSprite;
    [SerializeField] Sprite NPC1IconSprite;
    [SerializeField] Sprite NPC2IconSprite;
    [SerializeField] Sprite NPC3IconSprite;

    //[SerializeField] string npcName;
    //[SerializeField] IconeType npcIcon;


    // without the create system !!!!!!!!!! keep watching the codemonkey video about this 


    public enum IconeType
    {
        Player,
        NPC1,
        NPC2,
        NPC3,
        NPC4
    }


    private void Awake()
    {

        bubbleText = transform.Find("Text").GetComponent<TextMeshPro>();
        backgroundSpriteRenderer = transform.Find("Background").GetComponent<SpriteRenderer>();
        iconSpritRenderer = transform.Find("Icon").GetComponent<SpriteRenderer>();
    }

    //private void Start()
    //{
    //    DisplayNameAndIcon(npcIcon, npcName);
    //}


    void DisplayNameAndIcon(IconeType iconeType, string text)
    {
        bubbleText.SetText(text);

        bubbleText.ForceMeshUpdate(); // makes the text be applied fast
        Vector2 textSize = bubbleText.GetRenderedValues(false); // get the text size (length)

        Vector2 sizeOffset = new Vector2(0.2f, 0.2f);
        backgroundSpriteRenderer.size = textSize + sizeOffset; // matches the text background to the text lengths + an offset

        Vector3 positionOffset = new Vector3(0, 0, 0);
        backgroundSpriteRenderer.transform.localPosition = new Vector3(backgroundSpriteRenderer.size.x / 2, 0, 0) + positionOffset; // position the background in the middle of the text + an offset

        iconSpritRenderer.sprite = GetIconSprite(iconeType);

    }


    private Sprite GetIconSprite(IconeType iconeType)
    {
        switch (iconeType)
        {
            default:

            case IconeType.Player: return playerIconSprite;

            case IconeType.NPC1: return NPC1IconSprite;

            case IconeType.NPC2: return NPC2IconSprite;

            case IconeType.NPC3: return NPC3IconSprite;

        }
    }
}
