using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class SceneManager : MonoBehaviour
{
    public List<Land> lands;
    // zero to 100%
    public float alertLevel = 0;
    public float maxIntervalReduction; 
    public float baseInterval;
    float lastAlert = 0;

    public GameObject missilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var availableLands = lands.Where(x=> !x.isDestroyed);

        // check if lands = zero (end game)

        if( ((Time.time - lastAlert) * 1000) >  baseInterval - alertLevel * maxIntervalReduction ){
            LaunchThatMissileBaby(availableLands);
            lastAlert = Time.time;
        }
    }

    public void LaunchThatMissileBaby(IEnumerable<Land> availableLands){
        int landCount =  availableLands.Count();
        int source = UnityEngine.Random.Range(0, landCount);
        int target = UnityEngine.Random.Range(0, landCount);

        // last land destroys itself 
        if(target == source && landCount != 1){
            target = (target + 1) % landCount;
        }

        Land targetLand =  availableLands.ElementAt(target);
        Land sourceLand = availableLands.ElementAt(source);

        var missile = Instantiate(missilePrefab, sourceLand.transform.position, Quaternion.identity)
        .GetComponent<Missile>();

        missile.target = targetLand;
    }


}
