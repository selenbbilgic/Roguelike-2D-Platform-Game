using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public BoardManager BoardManager;
    public PlayerController PlayerController;

    public TurnManager TurnManager { get; private set;}
    public int m_FoodAmount = 50;

    public UIDocument UIDoc;
    private Label m_FoodLabel;
    private Label m_LevelLabel;
    public int CurrentLevel = 0;

    private VisualElement m_GameOverPanel;
    private Label m_GameOverMessage;
    private void Awake()
   {
       if (Instance != null)
       {
           Destroy(gameObject);
           return;
       }
      
       Instance = this;
   }
  
   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
{
   TurnManager = new TurnManager();
   TurnManager.OnTick += OnTurnHappen;
    m_LevelLabel = UIDoc.rootVisualElement.Q<Label>("LevelLabel");

   m_FoodLabel = UIDoc.rootVisualElement.Q<Label>("FoodLabel");
  
   m_GameOverPanel = UIDoc.rootVisualElement.Q<VisualElement>("GameOverPanel");
   m_GameOverMessage = m_GameOverPanel.Q<Label>("GameOverMessage");

   StartNewGame();
}

public void StartNewGame()
{
   m_GameOverPanel.style.visibility = Visibility.Hidden;
  
   CurrentLevel = 1;
   m_FoodAmount = 50;
   m_FoodLabel.text = "Food : " + m_FoodAmount;
  m_LevelLabel.text = "Level : " + CurrentLevel;
  BoardManager.Clean();
   BoardManager.Init();
  
   PlayerController.Init();
   PlayerController.Spawn(BoardManager, new Vector2Int(1,1));
}

    void OnTurnHappen()
    {
        ChangeFood(-1);
    }

    public void ChangeFood(int amount)
    {
    m_FoodAmount += amount;
    m_FoodLabel.text = "Food : " + m_FoodAmount;

    if (m_FoodAmount <= 0)
    {
        PlayerController.GameOver();
        m_GameOverPanel.style.visibility = Visibility.Visible;
        m_GameOverMessage.text = "Game Over!\n\nYou traveled through \n" + CurrentLevel + "\nlevels \n\n Press enter to restart";

    }
    }

    public void NewLevel()
    {
    BoardManager.Clean();
    BoardManager.Init();
    PlayerController.Spawn(BoardManager, new Vector2Int(1,1));

    CurrentLevel++;
    m_LevelLabel.text = "Level : " + CurrentLevel;
    }
}
