using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectPaper : MonoBehaviour
{
    public static Action OnPaperCollected;

    [SerializeField] [Min(1)] private float timeBeforeDisappearing = 1;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void OnMouseEnter()
    {
        StopAllCoroutines();

        spriteRenderer.enabled = true;

        #region Alpha value
        Color color = spriteRenderer.color;
        color.a = 1;
        spriteRenderer.color = color;
        #endregion
    }
    private void OnMouseOver()
    {
        if (!Input.GetMouseButton(0)) return;

            OnPaperCollected?.Invoke();
    }

    private void OnMouseExit()
    {
        StartCoroutine(HidePaper());
    }

    private IEnumerator HidePaper()
    {
        Color color = spriteRenderer.color;
        float counter = 0f;

      
        while (counter < timeBeforeDisappearing) {
            counter += Time.deltaTime;

           color.a = Mathf.Lerp(1, 0, counter / timeBeforeDisappearing);

            spriteRenderer.color = color;

            yield return null;
        }
    }
}
