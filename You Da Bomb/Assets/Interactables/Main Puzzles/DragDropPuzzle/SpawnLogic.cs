using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class WinLogic : MonoBehaviour
{
    [SerializeField] GameObject RedBlock;
    [SerializeField] GameObject BlueBlock;
    [SerializeField] GameObject GreenBlock;
    [SerializeField] GameObject S1;
    [SerializeField] GameObject S2;
    [SerializeField] GameObject S3;

    [SerializeField] GameObject RedItem;


    //private Vector3 spawn1 = new Vector3(-0.99f, 1.3f, 0.47f);
    private Vector3 spawn1;
    private Vector3 spawn2;
    private Vector3 spawn3;

    private List<Vector3> spawns = new List<Vector3>();
    

    public int SpawnPoint;
    // Start is called before the first frame update

    
    void Start()
    {
       spawn1 = S1.transform.position;
       spawn2 = S2.transform.position;
       spawn3 = S3.transform.position;
       
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
