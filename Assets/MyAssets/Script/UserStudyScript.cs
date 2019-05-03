using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserStudyScript: MonoBehaviour
{
    [SerializeField]
    private int count;
    [SerializeField]
    private int maxNumberOfTarget;

    private bool editModeTimerOn;
    private float editModeTimer;

    private bool authoringModeTimerOn;
    private float authoringModeTimer;

    private float deviceMovementDistance =0f;
    private float overallTime;


    public GameObject SlidARPP;
    public GameObject Hybrid;
    private SlidARPPController slidARScript;
    private HybridController hybridScript;
    private UserStudyUI userstudyUI;
    private DDAS dDAS;
    private GameObject cSelectedObject;

    public GameObject edit_Mode_timerText;
    public GameObject authoring_Mode_timerText;
    public GameObject device_Mov_DisText;
    // slidAR = 0;
    // hybrid = 1;
    [SerializeField]
    private int currentSystem;
    // Start is called before the first frame update

    [SerializeField]
    private GameObject[] targetListsNumber;

    [SerializeField]
    private int listsNumber;

    public bool userstudyBegin;
    [SerializeField]
    private GameObject currentTarget;


    [SerializeField]
    private bool userStudy = true;
    [SerializeField]
    private float timeOutLimited = 300f;

    void Start()
    {
        if (userStudy)
        {
            currentSystem = PlayerPrefs.GetInt("ChosenSystem");
            //targetGroup = PlayerPrefs.GetInt("TargetGroup");
            listsNumber = PlayerPrefs.GetInt("TargetGroup");
        }
        else
        {
            currentSystem = -1;
        }
        //Debug.Log("System "+currentSystem);
        //Debug.Log("TargetList "+listsNumber);


        userstudyUI = (UserStudyUI)gameObject.GetComponent<UserStudyUI>();
        dDAS = (DDAS)gameObject.GetComponent<DDAS>();

        if (currentSystem ==0)
        {
            SlidARPP.SetActive(true);
            slidARScript = (SlidARPPController)SlidARPP.GetComponent(typeof(SlidARPPController));
            slidARScript.AnnotationIsBeingSelected += EnableEditModeTimer;
            slidARScript.InteractInAuthoringMode += EnableAuthoringModeTimer;
            slidARScript.SendSelectedAnnotation += SetCurrentSelectedObject;
        }
        else if(currentSystem ==1)
        {
            Hybrid.SetActive(true);
            hybridScript = (HybridController)Hybrid.GetComponent(typeof(HybridController));
            hybridScript.AnnotationIsBeingSelected += EnableEditModeTimer;
            hybridScript.InteractInAuthoringMode += EnableAuthoringModeTimer;
            hybridScript.SendSelectedAnnotation += SetCurrentSelectedObject;
        }
        else
        {
            slidARScript = (SlidARPPController)SlidARPP.GetComponent(typeof(SlidARPPController));
            slidARScript.AnnotationIsBeingSelected += EnableEditModeTimer;
            slidARScript.InteractInAuthoringMode += EnableAuthoringModeTimer;
            slidARScript.SendSelectedAnnotation += SetCurrentSelectedObject;

            hybridScript = (HybridController)Hybrid.GetComponent(typeof(HybridController));
            hybridScript.AnnotationIsBeingSelected += EnableEditModeTimer;
            hybridScript.InteractInAuthoringMode += EnableAuthoringModeTimer;
            hybridScript.SendSelectedAnnotation += SetCurrentSelectedObject;
        }
        //timer = 0f;
        //TargetWithEvents.OnTargetAlignedWithAnnotation += TargetObjectIsAlignedWithAnnotation;
        //count = 0;

        //targetListsNumber[listsNumber].SetActive(true);
        //dDAS.SetupData(maxNumberOfTarget);
        //StartUserStudy(0);
    }

    public void StartUserStudy()
    {
        SetupUserStudyData();
    }

    public void SetupUserStudyData()
    {
        ResetUserStudy();


        targetListsNumber[listsNumber].SetActive(true);



        maxNumberOfTarget = targetListsNumber[listsNumber].transform.childCount - 1;
        dDAS.SetupData(maxNumberOfTarget);
        EnableTargetObject(0);
        userstudyBegin = true;
    }

    private void EnableTargetObject(int i)
    {
        if (i != 0)
        {
            DisableCurrentTarget();
        }

        currentTarget = targetListsNumber[listsNumber].transform.GetChild(i).gameObject;
        currentTarget.SetActive(true);
        //currentTarget.GetComponent<TargetWithEvents>().OnTargetAlignedWithAnnotation += TargetObjectIsAlignedWithAnnotation;
        //userstudyUI.RegisterNewListenser(currentTarget);
    }
    public void ShowCurrentTarget()
    {
        currentTarget.SetActive(true);
    }

    private void DisableCurrentTarget()
    {
        //currentTarget.GetComponent<TargetWithEvents>().OnTargetAlignedWithAnnotation -= TargetObjectIsAlignedWithAnnotation;
        //userstudyUI.ClearAll();
        currentTarget.SetActive(false);
        currentTarget = null;
    }


    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        if (userstudyBegin)
        {
            RunningUserStudy();
        }
    }

    private void RunningUserStudy()
    {
        if (count < maxNumberOfTarget)
        {
            if (editModeTimerOn)
            {
                CorrectionCheck();
                editModeTimer += Time.deltaTime;
                edit_Mode_timerText.GetComponent<Text>().text = editModeTimer.ToString("F2") + " S";

                if (editModeTimer >= timeOutLimited)
                {
                    TimeOut();
                }
                overallTime += Time.deltaTime;
                MeasureDeviceMovement();

            }
            if (authoringModeTimerOn)
            {
                authoringModeTimer += Time.deltaTime;
                authoring_Mode_timerText.GetComponent<Text>().text = authoringModeTimer.ToString("F2") + " S";
                overallTime += Time.deltaTime;
            }
        }
    }
    /*
     * Stop and continue after time out function  
     * */  
    private void TimeOut()
    {
        userstudyBegin = false;
        userstudyUI.ShowTimeOutText(true);
    }
    public void ContinueAfterTimeOut()
    {
        timer = 0;
        userstudyBegin = true;
        TargetObjectIsAlignedWithAnnotation();
        userstudyUI.ShowTimeOutText(false);
    }


    private float timer = 0;
    private float delayTime = 1f;

    private void CorrectionCheck()
    {
        if(CheckPosition()&&CheckOrientation()){
            timer += Time.deltaTime;

            if (timer >= delayTime)
            {
                //Debug.Log("Pos Correct");
                TargetObjectIsAlignedWithAnnotation();
                timer = 0;
                
            }
        }
        else
        {
            timer = 0;
        }
    }
    [SerializeField]
    private float minRot = 12f;
    private bool CheckOrientation()
    {
        if (cSelectedObject == null)
        {
            return false;
        }
        if (Quaternion.Angle(cSelectedObject.transform.rotation, currentTarget.transform.rotation) <= minRot)
        {
            return true;
        }

        return false;
    }
    [SerializeField]
    private float minDis = 3f;
    private bool CheckPosition()
    {
        if (cSelectedObject == null)
        {
            return false;
        }
        if (Vector3.Distance(cSelectedObject.transform.position, currentTarget.transform.position) <= minDis)
        {
            return true;

        }

        return false;
    }

    private float elapsedTime = 0f;
    private float setElapsedTime = 1f;
    private Vector3 prevDevicePos = new Vector3(0,0,0);
    private bool isFirstTimeCall = true;
    private void MeasureDeviceMovement()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= setElapsedTime)
        {
            if(!isFirstTimeCall && (Vector3.Distance(prevDevicePos, Camera.main.transform.position)>=1f))
            {
                deviceMovementDistance += Vector3.Distance(prevDevicePos, Camera.main.transform.position);
            }
            /*
            if (deviceMovementDistance > 0.0f)
            {
                deviceMovementDistance += Vector3.Distance(prevDevicePos, Camera.main.transform.position);
            }*/
            prevDevicePos = Camera.main.transform.position;
            elapsedTime = 0;
            device_Mov_DisText.GetComponent<Text>().text = deviceMovementDistance.ToString("F2") + " cm";

            if (isFirstTimeCall)
            {
                isFirstTimeCall = false;
            }
        }

    }

    public void ResetUserStudy()
    {
        count = 0;
        editModeTimer = 0f;
        deviceMovementDistance = 0f;
        isFirstTimeCall = true;
        authoringModeTimer = 0f;
        overallTime = 0f;
    }

    public void TargetObjectIsAlignedWithAnnotation()
    {
        //Debug.Log("Correct!!!!");


        SaveCurrentData();
        editModeTimer = 0f;
        deviceMovementDistance = 0f;
        isFirstTimeCall = true;
        authoringModeTimer = 0f;
        overallTime = 0f;
        EnableEditModeTimer(false);
        /*
        count++;
        if (count < maxNumberOfTarget)
        {
            EnableTargetObject(count);
        }*/
        if (!IsFinish())
        {
            //Debug.Log("Next target :"+count);
            EnableTargetObject(count);
        }
        else
        {
            //Debug.Log("Finish!!!!");
            DisableCurrentTarget();
            dDAS.UploadDataToInternet();
            userstudyUI.ShowFinishTesxt(true);
        }
        Destroy(cSelectedObject);
        //Debug.Log("Remove cObject");
    }

    private bool IsFinish()
    {
        count++;
        if(count+1 <= maxNumberOfTarget)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void SaveCurrentData()
    {
        dDAS.SaveTrialData(authoringModeTimer, editModeTimer, deviceMovementDistance,count,overallTime);
    }
    

    public void EnableAuthoringModeTimer(bool b)
    {
        authoringModeTimerOn = b;
    }

    public void EnableEditModeTimer(bool b)
    {
        //Debug.Log("EditModeTimer "+ b );
        editModeTimerOn = b;
    }
    private void SetCurrentSelectedObject(GameObject go)
    {
        cSelectedObject = go;
    }

    public void StopUserStudy()
    {
        ResetUserStudy();
        EnableEditModeTimer(false);
        EnableAuthoringModeTimer(false);
        userstudyBegin = false;
    }
}
