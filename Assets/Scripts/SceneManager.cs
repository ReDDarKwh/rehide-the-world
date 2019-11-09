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
    public float intervalReductionPerLevel;
    public float numberOfSecondsPerAlertLevel;
    public float baseInterval;
    float lastAlert = 0;
    public GameObject missilePrefab;
    public int maxMissileDifficulty;

    Missile selectedMissile;
    private bool endGame;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var availableLands = lands.Where(x => !x.isDestroyed);

        // check if lands = zero (end game)

        if (availableLands.Count() == 0)
        {
            endGame = true;
        }

        alertLevel = Time.time / numberOfSecondsPerAlertLevel;
        
        var missileCount = GameObject.FindGameObjectsWithTag("Missile").Count();

        if (!endGame && ( missileCount == 0 || (Time.time - lastAlert) > baseInterval - alertLevel * intervalReductionPerLevel))
        {

            LaunchThatMissileBaby(availableLands);
            
            lastAlert = Time.time;
        }
    }

    public void LaunchThatMissileBaby(IEnumerable<Land> availableLands)
    {
        int landCount = availableLands.Count();
        int source = UnityEngine.Random.Range(0, landCount);
        int target = UnityEngine.Random.Range(0, landCount);

        // last land destroys itself 
        if (target == source && landCount != 1)
        {
            target = (target + 1) % landCount;
        }

        Land targetLand = availableLands.ElementAt(target);
        Land sourceLand = availableLands.ElementAt(source);

        var missile = Instantiate(missilePrefab, sourceLand.transform.position, Quaternion.identity)
        .GetComponent<Missile>();

        missile.target = targetLand;
        missile.difficultyLevel = (int)UnityEngine.Random.value * maxMissileDifficulty;
        missile.manager = this;
    }

    internal void SetSelectedMissile(Missile missile)
    {
        if (selectedMissile != null)
        {
            selectedMissile.UnSelect();
        }
        selectedMissile = missile;
    }
}
