using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserStudyMainScene : MonoBehaviour
{

    /// 
    /// 0 = slidAR+
    /// 1 = Hybrid
    ///
    [SerializeField]
    private int ChosenMethod;


    /// 
    /// 0 = Pos_Easy
    /// 1 = Pos_Hard
    /// 2 = Target_Aligned_World_G
    /// 3 = Target_Not_Aligned_World_G
    /// 4 = Target_Aligned_World_Not
    /// 5 = Target_Not_Aligned_World_Not
    /// 
    [SerializeField]
    private int ChosenTarget;

    /*[SerializeField]
    private int ChosenWorldCoor;
    */

    public void SetChosenMethod(int i)
    {
        ChosenMethod = i;

        if(PlayerPrefs.HasKey("ChosenSystem")){
            PlayerPrefs.DeleteKey("ChosenSystem");
        }
        PlayerPrefs.SetInt("ChosenSystem",ChosenMethod);
    }

    public void SetChosenStage(int Stage)
    {
        if (PlayerPrefs.HasKey("WorldCoor") && PlayerPrefs.HasKey("TargetGroup"))
        {
            PlayerPrefs.DeleteKey("WorldCoor");
            PlayerPrefs.DeleteKey("TargetGroup");
        }

        if (Stage>=4)
        {
            PlayerPrefs.SetInt("WorldCoor", 1);

            if (Stage == 4)
            {
                PlayerPrefs.SetInt("TargetGroup", 2);
            }
            else
            {
                PlayerPrefs.SetInt("TargetGroup", 3);
            }

        }
        else
        {
            PlayerPrefs.SetInt("WorldCoor", 0);

            PlayerPrefs.SetInt("TargetGroup", Stage);
        }



        /*
        ChosenTarget = targetGroup;
        PlayerPrefs.SetInt("TargetGroup", ChosenTarget);
        */

    }

    public void LoadUserStudyScenes()
    {
        SceneManager.LoadScene("UserStudy");
    }
    public void LoadTutorialScenes()
    {
        SceneManager.LoadScene("Testing");
    }
    public void ChangeScenes(string name)
    {
        SceneManager.LoadScene(name);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    
    }
}
