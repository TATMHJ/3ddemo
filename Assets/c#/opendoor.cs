using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class opendoor : MonoBehaviour
{
    public static bool HasKey { get; private set; }

    [SerializeField] private KeyCode pickupKey = KeyCode.E;
    [SerializeField] private GameObject panel;

    private bool playerInRange;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        playerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        playerInRange = false;
    }

    private void Update()
    {
        if (!playerInRange)
        {
            return;
        }

        if (Input.GetKeyDown(pickupKey))
        {
            PickupKey();
        }
    }

    public void PickupKey()
    {
        if (HasKey)
        {
            return;
        }

        HasKey = true;
        Destroy(gameObject);
    }
}
