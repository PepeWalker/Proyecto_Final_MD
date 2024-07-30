using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCooldown : MonoBehaviour
{
    public bool cooldown = false;
    public float cooldownTime;

    public float scaleFactor=1.1f;
    public RectTransform rectTransform;
    public Vector3 originalScale;
    public Outline outline;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalScale = transform.localScale;

        outline.enabled = false;
    }

    public void OnButtonPressCooldown()
    {
        Invoke("ResetCooldown", cooldownTime);
        cooldown = true;
    }

    void ResetCooldown()
    {
        GetComponent<Button>().interactable= true;
        cooldown = false;

    }

    public void OnEnterIncreaseSize()
    {
        rectTransform.localScale = originalScale * scaleFactor;

        outline.enabled = true;
    }

    public void OnExitReduceSize()
    {
        rectTransform.localScale = originalScale;
        outline.enabled = false;
    }

    public void OnClickMiniAnimation()
    {

    }
}
