using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CatGrabsItemsGame : BaseGame
{
    /// <summary>
    /// The map that this game should instantiate.
    /// </summary>
    public MapController mapPrefab;
    protected MapController mapInstance;

    /// <summary>
    /// The cat character that this game should instantiate.
    /// </summary>
    public CatController catPrefab;
    protected CatController catInstance;

    /// <summary>
    /// The total number of targets.
    /// </summary>
    public int numTargets = 1;

    /// <summary>
    /// How many targets has beencaught.
    /// </summary>
    public int catchedTargets = 0;

    /// <summary>
    /// When the game started.
    /// </summary>
    protected float startGameTime = 0.0f;
    
    /// <summary>
    /// How much time is left.
    /// </summary>
    public float remainingTime = 0.0f;

    /// <summary>
    /// How long that game should run.
    /// </summary>
    public float gameLength = 5.0f;

    /// <summary>
    /// Rotates/scales the map using some gestures
    /// </summary>
    public MapManipulator mapManipulator;

    /// <summary>
    /// Indicates if a target was caught.
    /// Mainly for the UI to update itself.
    /// </summary>
    public UnityEvent OnTargetCatched = new UnityEvent();

    /// <summary>
    /// Indicates that the cat's health changed.
    /// Mainly for the UI to update itself.
    /// </summary>
    public CatController.HealthChangedEvent OnCatHealthChanged
    {
        get
        {
            return catInstance.OnHealthChanged;
        }

        set
        {
            catInstance.OnHealthChanged = value;
        }
    }

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
            EndGameplay();
        }
    }

    /// <summary>
    /// Ends the gameplay of this game. Doesn't kill the game.
    /// </summary>
    public override void EndGameplay()
    {
        state = State.ended;
        catInstance.gameObject.SetActive(false);
        mapInstance.mapRaycastController.enabled = false;
        OnGameplayEnded.Invoke();
    }

    /// <summary>
    /// TODO
    /// </summary>
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
            EndGameplay();
        }
    }

    /// <summary>
    /// Instantiates the map and character. Listens to some events.
    /// Introduces an artifical wait of one second to simulate some loading, since these prefabs are
    /// already at hand and not being loaded from AssetBundles.
    /// </summary>
    /// <param name="operation"></param>
    /// <returns></returns>
    protected IEnumerator CRDummyLoad(GameOperation operation)
    {
        yield return new WaitForSeconds(1.0f);
        mapInstance = GameObject.Instantiate(mapPrefab);
        mapManipulator.mapPivot = mapInstance.transform;
        SetupTargets();

        catInstance = GameObject.Instantiate(catPrefab);
        catInstance.transform.SetParent(mapInstance.transform);
        catInstance.transform.position = mapInstance.characterSpawnPoint.position;
        catInstance.transform.rotation = mapInstance.characterSpawnPoint.rotation;
        OnCatHealthChanged.AddListener(OnCatHealthChange);

        mapInstance.mapRaycastController.onRayCastHit.AddListener(catInstance.MoveTo);

        operation.isDone = true;
        state = State.loaded;
        OnGameLoaded.Invoke();
    }

    /// <summary>
    /// Checks if the health is less or equal thatn 0 meaning that an end game condition was met.
    /// </summary>
    /// <param name="newHealth"></param>
    protected void OnCatHealthChange(float newHealth)
    {
        if (newHealth <= 0.0f)
        {
            EndGameplay();
        }
    }

    /// <summary>
    /// Initialies the target counters.
    /// Listens to the callback that indicate that a fish was caught.
    /// </summary>
    protected void SetupTargets()
    {
        numTargets = mapInstance.targets.Length;
        catchedTargets = 0;
        for (int i = 0; i < mapInstance.targets.Length; i++)
        {
            mapInstance.targets[i].OnCatchedByCat.AddListener(TargetCatched);
        }
    }

    /// <summary>
    /// Unloads (destroy) the assets instantiates by this game.
    /// Introuces an artifical wait of one second.
    /// </summary>
    /// <param name="operation"></param>
    /// <returns></returns>
    protected IEnumerator CRDummyUnload(GameOperation operation)
    {
        Destroy(mapInstance.gameObject);
        yield return new WaitForSeconds(1.0f);
        OnGameQuit.Invoke();
        operation.isDone = true;
    }
}
