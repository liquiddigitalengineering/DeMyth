using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonMoving : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool isHovering = false;

    private Transform textChild;

    private void Awake()
    {
        textChild = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHovering) return;
        //get the objects current position and put it in a variable so we can access it later with less code
        Vector3 pos = textChild.transform.position;
        //calculate what the new Y position will be
        float newY = Mathf.Sin(Time.time * 5) * 0.5f + pos.y;
        //set the object's Y to the new calculated Y
        textChild.transform.position = new Vector3(pos.x, newY, pos.z);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if ( isHovering) return;
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;

        textChild.transform.localPosition = Vector3.zero;
    }


}
