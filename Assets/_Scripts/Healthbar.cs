using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Original HealthBar Script taken from Jason Weimann Unity tutorial

public class Healthbar : MonoBehaviour
{
    [SerializeField]
    private Image foregroundImage;
    [SerializeField]
    private float updateSpeedSeconds = 0.5f;

    //[SerializeField]
    private Canvas healthbarCanvas;

    //public Health health;

    Quaternion rotation;
    Vector3 positionOffset;

    private void Awake()
    {
        healthbarCanvas = GetComponent<Canvas>();
        GetComponentInParent<Health>().OnHealthPctChanged += HandleHealthChanged;

        //health = GetComponentInParent<Health>();
        //health.OnHealthPctChanged += HandleHealthChanged;
        //transform.Rotate(0, 0, 0);
        rotation = transform.parent.rotation;
        positionOffset = transform.localPosition;
    }

    private void OnDisable()
    {
        //health.OnHealthPctChanged -= HandleHealthChanged;
        // When the Gameobject gets disabled, reset the healthbar fill amount to full.
        foregroundImage.fillAmount = 1;
    }

    private void HandleHealthChanged(float pct)
    {
        if (healthbarCanvas != null && healthbarCanvas.gameObject.activeInHierarchy)
        {
            StartCoroutine(ChangeToPct(pct));
        }

            //StartCoroutine(ChangeToPct(pct));    
    }

    private IEnumerator ChangeToPct(float pct)
    {
        float preChangePct = foregroundImage.fillAmount;
        float elapsed = 0.0f;

        while (elapsed < updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            foregroundImage.fillAmount = Mathf.Lerp(preChangePct, pct, elapsed / updateSpeedSeconds);
            yield return null;
        }

        foregroundImage.fillAmount = pct;
    }

    private void LateUpdate()
    {
        // Make canvas face camera
        //transform.LookAt(Camera.main.transform);
        //transform.Rotate(0, 180, 0);
        transform.rotation = rotation;
        transform.position = transform.parent.position + positionOffset;
    }
}
