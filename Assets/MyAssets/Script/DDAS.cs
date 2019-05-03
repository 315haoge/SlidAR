using UnityEngine;
using UnityEngine.UI;

/*
 * Data Display and saving
 * */
public class DDAS : MonoBehaviour
{
    [SerializeField]
    private float[,] timeData;

    [SerializeField]
    private float[] movementData;

    [SerializeField]
    private float[] overallTimeData;

    private float tmp_Authoring_Time;
    private float tmp_Editing_Time;
    private float tmp_Movemnet_Dis;
    private float tmp_overall_Time;

    private float total_Authoring_Time ;
    private float total_Editing_Time;
    private float total_Movemnet_Dis;
    private float total_overall_Time;

    [SerializeField]
    private GameObject scorePref;

    [SerializeField]
    private GameObject scoringPannel;

    //private int index;
    //private int count;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupData(int i)
    {
        timeData = new float[i,2];
        movementData = new float[i];
        overallTimeData = new float[i];
        total_Editing_Time = 0;
        total_Movemnet_Dis = 0;
        total_Authoring_Time = 0;
        total_overall_Time = 0;
       //index = 0;
        //count = i;
    }

    public void SaveTrialData(float author_time, float edit_time, float move_dis,int index,float overall_time)
    {
        tmp_Authoring_Time = author_time;
        tmp_Editing_Time = edit_time;
        tmp_Movemnet_Dis = move_dis;
        tmp_overall_Time = overall_time;

        total_Authoring_Time += author_time;
        total_Editing_Time += edit_time;
        total_Movemnet_Dis += move_dis;
        total_overall_Time += overall_time;


        UpdateTotalScoreUI();
        AddNewScoringUI(index);

        timeData[index, 0] = tmp_Authoring_Time;
        timeData[index, 1] = tmp_Editing_Time;
        movementData[index] = tmp_Movemnet_Dis;
        overallTimeData[index] = tmp_overall_Time;
        //index++;
        /*
        if (index==count)
        {
            UploadDataToGoogleDrive();
        }*/
    }

    private GameObject tmpTotalScore;
    private void UpdateTotalScoreUI()
    {
        tmpTotalScore = scoringPannel.transform.GetChild(0).gameObject;
        tmpTotalScore.transform.GetChild(1).GetComponent<Text>().text = total_Authoring_Time.ToString("F2") + " s";
        tmpTotalScore.transform.GetChild(2).GetComponent<Text>().text =  total_Editing_Time.ToString("F2") + " s";
        tmpTotalScore.transform.GetChild(3).GetComponent<Text>().text = total_Movemnet_Dis.ToString("F2") + " cm";
        tmpTotalScore.transform.GetChild(4).GetComponent<Text>().text = total_overall_Time.ToString("F2") + " s";
    }
    private GameObject tmpTrialScore;
    private void AddNewScoringUI(int index)
    {
        tmpTrialScore = Instantiate(scorePref, scoringPannel.transform);
        tmpTrialScore.transform.GetChild(0).GetComponent<Text>().text = (index+1).ToString();
        tmpTrialScore.transform.GetChild(1).GetComponent<Text>().text = tmp_Authoring_Time.ToString("F2") + " s";
        tmpTrialScore.transform.GetChild(2).GetComponent<Text>().text = tmp_Editing_Time.ToString("F2") + " s";
        tmpTrialScore.transform.GetChild(3).GetComponent<Text>().text = tmp_Movemnet_Dis.ToString("F2") + " cm";
        tmpTrialScore.transform.GetChild(4).GetComponent<Text>().text = tmp_overall_Time.ToString("F2") + " s";
    }

    public void UploadDataToInternet()
    {

    }

    
    public void ClearScorePannel()
    {
        for (int i=0; i<scoringPannel.transform.childCount; i++)
        {
            if (i != 0)
            {
                Destroy(scoringPannel.transform.GetChild(i).gameObject);
            }
        }
    }

}
