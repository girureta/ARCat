using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatGrabsItemsGame : BaseGame
{
    public MapController mapPrefab;
    protected MapController mapInstance;

    public CatController catPrefab;
    protected CatController catInstance;

    public override GameOperation LoadGame()
    {
        GameOperation operation = new GameOperation();
        StartCoroutine(CRDummyLoad(operation));
        return operation;
    }

    public override void StartGame()
    {
        StartCoroutine(CRFinishGameEnd());
    }

    public override void PauseGame()
    {
    }

    public override GameOperation QuitGame()
    {
        GameOperation operation = new GameOperation();
        StartCoroutine(CRDummyUnload(operation));
        return operation;
    }

    protected IEnumerator CRDummyLoad(GameOperation operation)
    {
        yield return new WaitForSeconds(1.0f);
        mapInstance = GameObject.Instantiate(mapPrefab);

        catInstance = GameObject.Instantiate(catPrefab);
        catInstance.transform.SetParent(mapInstance.transform);
        catInstance.transform.position = mapInstance.characterSpawnPoint.position;
        catInstance.transform.rotation = mapInstance.characterSpawnPoint.rotation;

        mapInstance.mapRaycastController.onRayCastHit.AddListener(catInstance.MoveTo);

        operation.isDone = true;
    }

    protected IEnumerator CRDummyUnload(GameOperation operation)
    {
        Destroy(mapInstance.gameObject);
        yield return new WaitForSeconds(1.0f);
        OnGameFinished.Invoke();
        operation.isDone = true;
    }

    protected IEnumerator CRFinishGameEnd()
    {
        yield return new WaitForSeconds(1.0f);
        QuitGame();
    }
}
