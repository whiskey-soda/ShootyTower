using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

[RequireComponent(typeof(TextMeshPro))]

public class DamageNumber_System : MonoBehaviour
{
    public float damageValue;
    TextMeshPro damageText;

    [SerializeField] float duration = .3f;

    private void Awake()
    {
        damageText = GetComponent<TextMeshPro>();
    }

    private void Start()
    {
        damageText.text = damageValue.ToString();
    }

    private void FixedUpdate()
    {
        duration -= Time.deltaTime;

        if (duration <= 0)
        {
            Destroy(gameObject);
        }
    }

}
