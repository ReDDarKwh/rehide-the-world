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
    private int landsTotal;
    private bool canSelect = true;

    // Start is called before the first frame update
    void Start()
    {
        landsTotal = lands.Count;

        foreach (var land in lands)
        {
            land.destroyed += OnLandDestoyed;
        }
    }

    internal void WonMiniGame()
    {
        UnSelectSelectedMissile();
        selectedMissile.DetonateMissile();
    }

    void UnSelectSelectedMissile()
    {
        selectedMissile.UnSelect();
       
    }

    private void OnLandDestoyed(object sender, Land e)
    {
        alertLevel += 1;
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

        alertLevel += Time.deltaTime / numberOfSecondsPerAlertLevel;

        var missileCount = GameObject.FindGameObjectsWithTag("Missile").Count();

        if (!endGame && (missileCount == 0 || (Time.time - lastAlert) > baseInterval - alertLevel * intervalReductionPerLevel))
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

        missile.miniGameIndex = 1;
        missile.selected += OnMissileSelected;
    }

    private void OnMissileSelected(object sender, Missile e)
    {

        StartCoroutine("SetSelectedMissile", e);
    }

    internal IEnumerator SetSelectedMissile(Missile missile)
    {
        if (canSelect)
        {
            if (selectedMissile != null)
            {
                UnSelectSelectedMissile();
                 var operation = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(selectedMissile.miniGameIndex);
            }

            missile.Select();
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(missile.miniGameIndex, UnityEngine.SceneManagement.LoadSceneMode.Additive);
            selectedMissile = missile;
            Debug.Log("missile selected");
            canSelect = false;

            yield return new WaitForSeconds(0.1f);
            canSelect = true;
        }
    }
}
