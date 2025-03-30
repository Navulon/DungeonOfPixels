using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingDamages : MonoBehaviour
{
    [HideInInspector] public float damage;
    private TextMesh textMesh;

    private void Start()
    {
        textMesh = GetComponent<TextMesh>();
        textMesh.text = "-" + damage;
    }

    public void OnAnimationOver()
    {
        Destroy(gameObject);
    }
}