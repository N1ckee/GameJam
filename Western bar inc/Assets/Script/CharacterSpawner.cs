using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public int ChractersInWorld;

    public bool SpawnCharacter = false;
    public bool TrainPass;
    public ShopStats TheShopStats;
    public GameObject Character;
    public Transform Pos;
    public Animator Train;

    private float wait = 30f;
    private float time = 30f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (time >= wait && ChractersInWorld < 3 + TheShopStats.Seats)
        {
            Train.SetTrigger("Spawn");
            time = 0;
        }
        if (SpawnCharacter)
        {
            Spawn();
            SpawnCharacter = false;
        }

        time += Time.fixedDeltaTime;
    }

    public void Spawn()
    {
        if (ChractersInWorld < 3 + TheShopStats.Seats)
        {
            ChractersInWorld++;
        Instantiate<GameObject>(Character, Pos.position, new Quaternion());
        }
    }
}
