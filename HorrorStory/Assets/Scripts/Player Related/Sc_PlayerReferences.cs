using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sc_PlayerReferences : MonoBehaviour
{
    #region Singleton Pattern variables
    private static Sc_PlayerReferences _instance;
    public static Sc_PlayerReferences Instance { get { return _instance; } }
    #endregion
    
    #region Value related variables
    public int maxHealth;
    public float walkingSpeed;
    public float runningSpeed;
    public bool isDead;
    public bool isInCutscene;
    public float gravity;
    public Vector3 moveTowardsPos;
    public float jumpAndFallVelocity;
    public float interactionDistance;
    public bool successfulHit;
    public bool hasLens;
    public bool lensShowing;
    public bool firstUseLens;
    #endregion

    #region Component variable list
    public Camera mainCamera;
    public Rigidbody rb;
    public Collider collider;
    public TMPro.TextMeshProUGUI interactionText;
    public GameObject holdButton;
    public UnityEngine.UI.Image holdButtonProgress;
    public GameObject lensGameObject;
    
    public LayerMask groundLayerMask;
    #endregion

    #region Sound clips

    //Do be added

    #endregion

    private void Awake()
    {
        //Making Sc_PlayerReferences.Instance return this script.
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        
        Time.timeScale = 1; //Putting the Time scale to 1 since if the game ends, we have to make sure its restarted properly.

        //Making sure that the components are found even if not added in the scene. References to them will be accessible with the Instance.
        rb = gameObject.GetComponent<Rigidbody>();
        collider = gameObject.GetComponent<Collider>();
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        interactionText = GameObject.FindWithTag("InteractionUI").GetComponent<TMPro.TextMeshProUGUI>();
        lensGameObject = GameObject.FindWithTag("Lens");
        holdButton = GameObject.FindWithTag("HoldButtonUI");
        holdButtonProgress = GameObject.FindWithTag("ProgressUI").GetComponent<Image>();

    }
}
