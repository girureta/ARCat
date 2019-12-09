using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CatGrabsItemsGame : BaseGame
{
    public MapController mapPrefab;
    protected MapController mapInstance;

    public CatController catPrefab;
    protected CatController catInstance;

    public int numTargets = 1;
    public int catchedTargets = 0;

    protected float startGameTime = 0.0f;
    public float remainingTime = 0.0f;
    public float gameLength = 5.0f;

    public UnityEvent OnTargetCatched = new UnityEvent();

    public override GameOperation LoadGame()
    {
        GameOperation operation = new GameOperation();
        StartCoroutine(CRDummyLoad(operation));
        return operation;
    }

    public override void StartGame()
    {
        startGameTime = Time.time;
        state = State.started;
    }

    private void Update()
    {
        if (state != State.started)
            return;
        remainingTime = gameLength - (Time.time - startGameTime);

        //The timer ran out
        if (remainingTime <= 0.0f)
        {
            QuitGame();
        }
    }

    public override void PauseGame()
    {
    }

    public override GameOperation QuitGame()
    {
        state = State.quit;
        GameOperation operation = new GameOperation();
        StartCoroutine(CRDummyUnload(operation));
        return operation;
    }

    protected void TargetCatched()
    {
        catchedTargets++;
        OnTargetCatched.Invoke();
        if (catchedTargets == numTargets )
        {
            QuitGame();
        }
    }

    protected IEnumerator CRDummyLoad(GameOperation operation)
    {
        yield return new WaitForSeconds(1.0f);
        mapInstance = GameObject.Instantiate(mapPrefab);
        SetupTargets();

        catInstance = GameObject.Instantiate(catPrefab);
        catInstance.transform.SetParent(mapInstance.transform);
        catInstance.transform.position = mapInstance.characterSpawnPoint.position;
        catInstance.transform.rotation = mapInstance.characterSpawnPoint.rotation;

        mapInstance.mapRaycastController.onRayCastHit.AddListener(catInstance.MoveTo);

        operation.isDone = true;
        state = State.loaded;
        OnGameLoaded.Invoke();
    }

    protected void SetupTargets()
    {
        numTargets = mapInstance.targets.Length;
        catchedTargets = 0;
        for (int i = 0; i < mapInstance.targets.Length; i++)
        {
            mapInstance.targets[i].OnCatchedByCat.AddListener(TargetCatched);
        }
    }

    protected IEnumerator CRDummyUnload(GameOperation operation)
    {
        Destroy(mapInstance.gameObject);
        yield return new WaitForSeconds(1.0f);
        OnGameFinished.Invoke();
        operation.isDone = true;
    }
}
