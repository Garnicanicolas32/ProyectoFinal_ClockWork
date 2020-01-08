using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawn : MonoBehaviour
{

    public Weapon_2[] HighChanceWeapons;
    public Weapon_2[] MidChanceWeapons;
    public Weapon_2[] LowChanceWeapons;
    private Vector2 pos;
    private Weapon_2 lastWeapon;

    public float WeaponSize = 1;

    public float Distance;
    public float TimeForSpawns;
    public float Recovery;

    private int[] Lenghts;


    // Use this for initialization
    void Start()
    {
        pos = transform.position;
        pos.y += Distance;
        Lenghts = new int[3];
        Recovery = TimeForSpawns;

        //HighChance
        Lenghts[2] = HighChanceWeapons.Length;
        //MidChance
        Lenghts[1] = MidChanceWeapons.Length;
        //LowChance
        Lenghts[0] = LowChanceWeapons.Length;

        Select();

    }
    void FixedUpdate()
    {
        if (!lastWeapon.onStand)
        {
            if (Recovery <= 0)
            {
                Recovery = TimeForSpawns;
                Select();
            }
            else
            {
                Recovery -= Time.deltaTime;
            }
        }

    }

    public void Select()
    {
        Weapon_2 send = null;
        int a = Random.Range(1, 7);

        if (a == 1 && Lenghts[0] != 0)
        {
            int i = Random.Range(0, Lenghts[0]);
            send = LowChanceWeapons[i];
        }
        else
        {

            if (a > 1 && a < 4 && Lenghts[1] != 0)
            {
                int i = Random.Range(0, Lenghts[1]);
                send = MidChanceWeapons[i];
            }
            else
            {
                if (Lenghts[2] != 0)
                {
                    int i = Random.Range(0, Lenghts[2]);

                    send = HighChanceWeapons[i];
                }
            }
        }
        if (send != null)
        {
            Spawn(send);
        }
    }

    public void Spawn(Weapon_2 spawn)
    {
        spawn.onStand = true;
        spawn.standPos = pos;
        lastWeapon = Instantiate(spawn, pos, Quaternion.identity);
        Vector3 scale = lastWeapon.transform.localScale;
        scale.x = WeaponSize;
        scale.y = WeaponSize;
        lastWeapon.transform.localScale = scale;
    }
}
