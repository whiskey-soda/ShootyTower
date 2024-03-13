using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TackDespawn : MonoBehaviour
{

    public float maxLifetime = .15f;
    public float lifetimeRemaining;

    // Start is called before the first frame update
    void Start()
    {
        lifetimeRemaining = maxLifetime;
    }

    // Update is called once per frame
    void Update()
    {
        if (lifetimeRemaining > 0)
        {
            lifetimeRemaining -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
