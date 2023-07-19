using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{

    public bool SpawnCharacter = false;
    public bool TrainPass;
    public GameObject Character;
    public Transform Pos;
    public Animator Train;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TrainPass)
        {
            Train.SetTrigger("Spawn");
            TrainPass = false;
        }
        if (SpawnCharacter)
        {
            Spawn();
            SpawnCharacter = false;
        }
    }

    public void Spawn()
    {
        Instantiate<GameObject>(Character, Pos.position, new Quaternion());
    }
}
