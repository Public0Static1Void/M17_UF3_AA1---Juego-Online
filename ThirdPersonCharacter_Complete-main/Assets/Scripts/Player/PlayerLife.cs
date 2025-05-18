using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerLife : NetworkBehaviour
{
    public NetworkVariable<int> player_life = new NetworkVariable<int>();
    private int max_hp = 100;

    public bool invulnerable = false;

    private float timer = 0;

    private Vector3 start_pos;

    Rigidbody rb;

    private void Start()
    {
        if (!IsOwner) return;
        player_life.Value = max_hp;

        start_pos = transform.localPosition;

        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!IsOwner) return;

        if (player_life.Value <= 0)
        {
            player_life.Value = max_hp;
            rb.isKinematic = true;
            transform.localPosition = start_pos;
        }

        if (invulnerable)
        {
            timer += Time.deltaTime;
            if (timer >= 0.5f)
            {
                invulnerable = false;
                rb.isKinematic = false;
                timer = 0;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!IsOwner) return;

        if (!invulnerable && other.CompareTag("Bullet"))
        {
            player_life.Value -= 21;
            if (player_life.Value <= 0)
            {
                player_life.Value = max_hp;
                rb.isKinematic = true;
                transform.localPosition = start_pos;
            }
            invulnerable = true;
        }
    }
}
