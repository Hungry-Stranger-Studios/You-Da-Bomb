using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class WinLogic : MonoBehaviour
{
    [SerializeField] GameObject RedBlock;
    [SerializeField] GameObject BlueBlock;
    [SerializeField] GameObject GreenBlock;

    [SerializeField] GameObject RedItem;

    private Vector3 spawn1 = new Vector3(-0.99f, 1.3f, 0.47f);
    private Vector3 spawn2 = new Vector3(-0.984f, 0.534f, 0.47f);
    private Vector3 spawn3 = new Vector3(-0.964f, -0.283f, 0.648f);

    private List<Vector3> spawns = new List<Vector3>();
    

    public int SpawnPoint;
    // Start is called before the first frame update

    
    void Start()
    {
       spawns.Add(spawn1);
       spawns.Add(spawn2);
       spawns.Add(spawn3);

       ChooseSpawn(RedBlock);
       ChooseSpawn(BlueBlock);
       ChooseSpawn(GreenBlock);
    }

    private void ChooseSpawn(GameObject block)
    {
        int randomNum = Random.Range(0, spawns.Count);

        block.transform.position = spawns[randomNum];

        spawns.RemoveAt(randomNum);
    }

   
}
