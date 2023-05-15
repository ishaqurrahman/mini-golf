using UnityEngine;


public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public GameObject ballPrefab;        
    public Vector3 ballSpawnPos;          

    public LevelData[] levelDatas;       

    private int shotCount = 0;              

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SpawnLevel(int levelIndex)
    {
       
        Instantiate(levelDatas[levelIndex].levelPrefab, Vector3.zero, Quaternion.identity);
        shotCount = levelDatas[levelIndex].shotCount;                               
        UIManager.instance.ShotText.text = shotCount.ToString();                   
                                                                  
        GameObject ball = Instantiate(ballPrefab, ballSpawnPos, Quaternion.identity);
        CameraFollow.instance.SetTarget(ball);                     
        GameManager.singleton.gameStatus = GameStatus.Playing;      
    }


    public void ShotTaken()
    {
        if (shotCount > 0)                                       
        {
            shotCount--;                                           
            UIManager.instance.ShotText.text = "" + shotCount;     

            if (shotCount <= 0)                                     
            {
                LevelFailed();                                       
            }
        }
    }

    
    public void LevelFailed()
    {
        if (GameManager.singleton.gameStatus == GameStatus.Playing)      
        {
            GameManager.singleton.gameStatus = GameStatus.Failed;   
            UIManager.instance.GameResult();                       
        }
    }

   
    public void LevelComplete()
    {
        if (GameManager.singleton.gameStatus == GameStatus.Playing) 
        {    
            if (GameManager.singleton.currentLevelIndex < levelDatas.Length)    
            {
                GameManager.singleton.currentLevelIndex++;  
            }
            else
            {
                
                GameManager.singleton.currentLevelIndex = 0;
            }
            GameManager.singleton.gameStatus = GameStatus.Complete; 
            UIManager.instance.GameResult();                        
        }
    }
}
