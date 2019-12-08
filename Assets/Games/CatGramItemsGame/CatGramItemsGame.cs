using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatGramItemsGame : BaseGame
{
    public GameObject mapPrefab;
    protected GameObject mapInstance;
    
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
        yield return new WaitForSeconds(3.0f);
        QuitGame();
    }
}
