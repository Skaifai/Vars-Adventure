using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.AI;

public class Program : MonoBehaviour
{
    // Timer support.
    Timer timer;

    #region Observer Pattern Fields
    
    // Trigger colliders game object in the scene, it contains every trigger collider
    [SerializeField]
    private GameObject zoneChecker;

    private ObserverA observerA;
    private ObserverB observerB;
    private SomeSubject someSubject;

    #endregion
    [SerializeField]
    private KnightObserver knightObserver;

    [SerializeField]
    private Animator knightAnimator;

    [SerializeField]
    private SpriteRenderer knightSpriteRenderer;

    #region Singleton Pattern Fields

    SpriteMaskController spriteMaskController1;
    SpriteMaskController spriteMaskController2;

    #endregion

    #region Command Pattern Fields

    [SerializeField]
    GameObject agentObject;

    NavMeshAgent navMeshAgentComp;

    AgentMovement agentMovement;
    #endregion

    private void Awake()
    {
        #region Command Pattern Implementation

        // Getting a Navigation Mesh Agent component from the game object selected in the editor 
        navMeshAgentComp = agentObject.GetComponent<NavMeshAgent>();

        // Restricting rotation and up axis of the NavMeshAgent
        navMeshAgentComp.updateRotation = false;
        navMeshAgentComp.updateUpAxis = false;

        // Assigning the AgentMovement class an object.
        // This class serves as an Invoker of the commands
        agentMovement = new AgentMovement(navMeshAgentComp);
        #endregion
    }
    // Start is called before the first frame update.
    void Start()
    {
        #region Singleton Pattern Support

        spriteMaskController1 = SpriteMaskController.GetInstance();

        spriteMaskController2 = SpriteMaskController.GetInstance();

        #endregion

        #region Observer Pattern Implementation

        // Concrete observers. Adding them to the main camera at the start of the game
        observerA = gameObject.AddComponent<ObserverA>();
        observerB = gameObject.AddComponent<ObserverB>();

        // Concrete subject. Getting it from the referenced gameobject in the editor
        someSubject = zoneChecker.GetComponent<SomeSubject>();

        // Subscribing observers to the subject
        someSubject.Subscribe(observerA);
        someSubject.Subscribe(observerB);

        // Adding the timer component to the gameobject.
        timer = gameObject.AddComponent<Timer>();

        // Setting time to 5 secs and starting the timer
        timer.Duration = 5f;
        timer.Run();

        #endregion

        agentMovement.Subscribe(knightObserver);
    }

    // Update is called once per frame.
    void Update()
    {
        #region Observer Pattern Implementation

        // Every 5 seconds the program will
        if (timer.Finished)
        {
            someSubject.DoSomething();
            timer.Run();
        }

        #endregion

        #region Singleton Pattern Implementation

        //if (spriteMaskController1 == spriteMaskController2)
        //    Debug.Log("Singleton is working properly!");

        #endregion

        #region Command Pattern Implementation

        agentMovement.ListenForCommands();
        agentMovement.ProcessCommands();

        #endregion
    }
}
