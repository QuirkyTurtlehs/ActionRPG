using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainVector : MonoBehaviour
{
    // Start is called before the first frame update

    PlayerInput playerInput;
    public GameObject player;

    public GameObject vectorModelPrefab;

    Material vectorMaterial;

    Color defaultColor;

    Vector3 targetPos = Vector3.zero;
    Vector3 startPos = Vector3.zero;

    Vector3 setupVelocity = Vector3.zero;
    Vector3 sliceVelocity = Vector3.zero;
    Vector3 followVelocity = Vector3.zero;

    public float setupTime = 0.4f;
    public float sliceTime = 0.2f;
    public float followTime = 0.5f;
    public float vectorRange = 6f;
    public float colorLerpTime = 2f;
    public float lethalRadius = 0.5f;

    float setupTimer;
    float sliceTimer;


    bool inMotion = false;

    bool slicing = false;
    bool settingUp = false;
    bool isTwoStepSlice = false;
    bool followPlayer = true;
    bool isLethal = false;

    void Start()
    {     
        vectorMaterial = vectorModelPrefab.GetComponent<Renderer>().material;
        defaultColor = vectorMaterial.color;
    }
    // Update is called once per frame
    void Update()
    {
        CheckRange();

        if (settingUp)
        {
            SetupSlice();
        }
        if (isTwoStepSlice)
        {
            SetupTwoStepSlice();
        }
        if (slicing)
        {
            Debug.Log("do slice");
            PerformSlice();
        }
        if (isLethal)
        {
            //CurrentlyLethal();
        }
        if (followPlayer)
        {
            transform.position = Vector3.SmoothDamp(transform.position, 
                new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z),
                ref followVelocity, followTime);
        }
        
    }

    //void CurrentlyLethal()
    //{
    //    RaycastHit[] hits = Physics.SphereCastAll(transform.position, lethalRadius, Vector3.zero);

    //    for (int i = 0; i < hits.Length; i++)
    //    {
    //        RaycastHit hit = hits[i];

    //        if (hit.collider.gameObject.tag == "Enemy")
    //        {
    //            Debug.Log("death");
    //        }
    //    }
    //}

    void CheckRange()
    {      
        Color FarAway = Color.red;

        if (Vector3.Distance(transform.position, player.transform.position) > vectorRange * 0.66)
        {
            vectorMaterial.color = Color.Lerp(vectorMaterial.color, FarAway, colorLerpTime);
        }
        if (Vector3.Distance(transform.position, player.transform.position) < vectorRange * 0.66)
        {
            vectorMaterial.color = Color.Lerp(vectorMaterial.color, defaultColor, colorLerpTime);
        }

        if (Vector3.Distance(transform.position, player.transform.position) > vectorRange)
        {
            followPlayer = true;
            isTwoStepSlice = false;
        }
        
        
    }

    public void StartSlice(Vector3 startLoc, Vector3 targetLoc)
    {
        targetPos = targetLoc;
        if (!inMotion)
        {
            setupTimer = setupTime;
            sliceTimer = sliceTime;

            startPos = startLoc;
            followPlayer = false;
            settingUp = true;

        }
    }

    public void TwoStepSlice(Vector3 startLoc, Vector3 targetLoc, bool firstPress)
    {
        ReleaseVector();
        
        if (firstPress)
        {
            slicing = false;
            setupTimer = setupTime;
            startPos = startLoc;
            startPos.y = 0.5f;

            isTwoStepSlice = true;
            followPlayer = false;
            
        }
        else if(!firstPress && !followPlayer)
        {
            targetPos = targetLoc;
            sliceTimer = sliceTime;
            isTwoStepSlice = false;
            settingUp = false;
            slicing = true;
            followPlayer = true;
        }
    }

    void SetupTwoStepSlice()
    {
        inMotion = true;
        transform.position = Vector3.SmoothDamp(transform.position, startPos, ref setupVelocity, setupTime);
    }

    void SetupSlice()
    {
        ReleaseVector();

        inMotion = true;
        transform.position = Vector3.SmoothDamp(transform.position, startPos, ref setupVelocity, setupTime);

        setupTimer -= Time.deltaTime;

        if (setupTimer <= 0)
        {
            settingUp = false;
            slicing = true;
        }
    }
    void PerformSlice()
    {       
        targetPos.y = 0.5f;

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref sliceVelocity, sliceTime);

        sliceTimer -= Time.deltaTime;
        if (sliceTimer <= 0)
        {
            slicing = false;
            inMotion = false;
            followPlayer = true;
            Debug.Log("done");
        }
    }
    void ReleaseVector()
    {
        transform.parent = null;
    }
    void BringBackVector()
    {
        transform.parent = player.transform;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("death");
        }
    }
}
