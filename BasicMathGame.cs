using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BasicMathGame : MonoBehaviour {

    enum Stages { TitleScreen, GameScreen, GameOverScreen, OptionScreen }

    public AudioClip buy_02;
    public AudioClip click_01;
    public AudioClip click_02;
    public AudioClip time_01;
    private AudioSource GameSound;
    public Texture2D Logo;
    public Texture2D Frame;
    public Texture2D SGSLogo;
    
    public GUIStyleState MathStyle;
    GUIStyle Big;
    GUIStyle Title;
    Stages stages;
    public Font MathWork;
    //Variables
    int A; int B; int C; float D; int score; int high = 0; int signs; float result; float Clock = 120.00f;
    string ProblemType; float multiplier; private int maxDigits = 11;
    AudioListener MathListen; float GameVolume = 1.0f;
    private string Answer = "0";

    void Start()
    {
        Big = new GUIStyle(); Title = new GUIStyle();
      
        Big.fontSize = 72;
        Big.font = MathWork;
      
        Title.font = MathWork;

        Cursor.visible = true;
        Title.fontSize = 72;
        Title.fontStyle = FontStyle.Italic;
        genQuestion();
        GameClock();
        GameSound = GetComponent<AudioSource>();
        stages = Stages.TitleScreen;
       
        
    }

    void Update()
    {
        GameClock();
        GameSound.GetComponent<AudioSource>().volume = GameVolume;
       
        

    }
        
    

    void OnGUI()
    {
        GUI.skin.button = Title;
        GUI.skin.label = Big;
        
        Title.normal.background = Frame;
        Title.normal.textColor = Color.black;
        Title.hover.textColor = Color.green;
       Title.hover.background = Frame;
        Big.normal.background = Frame;
        Big.hover.background = Frame;
        Big.normal.textColor = Color.green;
        Big.hover.textColor = Color.red;
        
        GUI.skin.settings.cursorColor = Color.black;
        Answer = GUILayout.TextArea(Answer);


        if (stages == Stages.TitleScreen)
        {
            GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height/2),Logo);
            //GUI.Label(new Rect(20, 20, 250, 250), "Simple Math Game", Title);
            if (GUI.Button(new Rect(150, 550, 320, 150), "Start Game"))
            {
                
                stages = Stages.GameScreen;
               
            }
            if (GUI.Button(new Rect(750, 550, 320, 150), "Options"))
            {

                stages = Stages.OptionScreen;
            }

            if (GUI.Button(new Rect(150, 650, 320, 150), "Quit"))
            {
                
                Application.Quit();
            }
        }

        if (stages == Stages.OptionScreen)
        {
            GUI.Label(new Rect(0,250,320,300),"Volume",Big);
            GameVolume = GUI.HorizontalSlider(new Rect(350, 250, 256, 32), GameVolume, 0.0F, 1.0F);

            if (GUI.Button(new Rect(200, 130, 100, 100), "120", Big))
            {
                Clock = 120;


            }

            if (GUI.Button(new Rect(400, 130, 100, 100), "240", Big))
            {
                Clock = 240;


            }
            if (GUI.Button(new Rect(600, 130, 100, 100), "480", Big))
            {
                Clock = 480;


            }

            if (GUI.Button(new Rect(800, 130, 100, 100), "960", Big))
            {
                Clock = 960;

            }

            

            if (GUI.Button(new Rect(500, 350, 320, 150), "Back"))
            {

                stages = Stages.TitleScreen;
            }


        }



        if (stages == Stages.GameScreen)
        {
            
            GUI.DrawTexture(new Rect(0, 0, 100,100), SGSLogo);
            Answer = GUI.TextField(new Rect(250, 300, 300, 100), Answer, Big);
            GUI.Label(new Rect(100, 100, 160, 160),  ProblemType, Title);
            GUI.Label(new Rect(560, 100, 300, 100), "Score:   " + score.ToString(), Title);
            GUI.Label(new Rect(560, 200, 300, 100), "Timer:   " + Clock.ToString("0"), Title);
            Cursor.visible = true;
            if (GUI.Button(new Rect(600, 300, 300, 100), "Restart",Big))
            {
                //Clock = 120.00f;
                score = 0;
                Answer = "0";
                genQuestion();
                GameClock();
                GameSound.PlayOneShot(click_01, 0.7f);
                Cursor.visible = true;
            }

            if (GUI.Button(new Rect(600, 700, 300, 100), "Quit",Big))
            {

                GameSound.PlayOneShot(click_02, 0.7f);
                stages = Stages.GameOverScreen;
            }

            if (GUI.Button(new Rect(600, 600, 300, 100), "Submit",Big))
            {
                CheckAnswer();
                Answer = "0";
                Cursor.visible = true;
            }

            if (GUI.Button(new Rect(600, 450, 300, 100), "Next",Big))
            {

                genQuestion();
                Answer = "0";
                Cursor.visible = true;
                GameSound.PlayOneShot(click_01, 0.7f);
            }

            if (GUI.Button(new Rect(208, 606, 70, 70), "1",Big))
                AppendNumber("1");
            if (GUI.Button(new Rect(361, 606, 70, 70), "2", Big))
                AppendNumber("2");
            if (GUI.Button(new Rect(513, 606, 70, 70), "3", Big))
                AppendNumber("3");
            if (GUI.Button(new Rect(208, 506, 70, 70), "4", Big))
                AppendNumber("4");
            if (GUI.Button(new Rect(361, 506, 70, 70), "5", Big))
                AppendNumber("5");
            if (GUI.Button(new Rect(513, 506, 70, 70), "6", Big))
                AppendNumber("6");
            if (GUI.Button(new Rect(208, 406, 70, 70), "7", Big))
                AppendNumber("7");
            if (GUI.Button(new Rect(361, 406, 70, 70), "8", Big))
                AppendNumber("8");
            if (GUI.Button(new Rect(513, 406, 70, 70), "9", Big))
                AppendNumber("9");
            if (GUI.Button(new Rect(361, 706, 70, 70), "0", Big))
                AppendNumber("0");
            if (GUI.Button(new Rect(208, 706, 70, 70), "-", Big))
                AppendNumber("-");


        }

        if (stages == Stages.GameOverScreen)
        {

            GUI.Label(new Rect(20, 140, 160, 30), "Your final score is " + score.ToString(), Big);

            GUI.Label(new Rect(200, 220, 160, 30), " Play Again?", Big);

            if (GUI.Button(new Rect(450, 300, 320, 100), "Start"))
            {
                stages = Stages.GameScreen;
                Clock = 120.00f;
                score = 0;
                genQuestion();
                GameClock();
                Cursor.visible = true;
                GameSound.PlayOneShot(click_01, 0.7f);
            }

            if (GUI.Button(new Rect(450, 620, 320, 100), "Quit"))
            {
                stages = Stages.TitleScreen;
            }
        }

    }

   

    private void AppendNumber(string s)
    {
        if ((Answer == "0") )
            Answer = (s == ".") ? "0." : s;
        else
            if (Answer.Length < maxDigits)
            Answer += s;

       
    }



    private void genQuestion()
    {
        A = Random.Range(0, 10); B = Random.Range(0, 10) + 1; C = Random.Range(0,100); signs = Random.Range(0, 7);
        D = Mathf.Pow(B,2);
        if (signs == 0)
        {
            ProblemType = A + "+" + B;
        }
        if (signs == 1)
        {
            ProblemType = A + "-" + B;
        }
        if (signs == 2)
        {
            ProblemType = A + "×" + B;
        }
        if (signs == 3)
        {
            ProblemType = C + "÷" + B;
        }
        if (signs == 4)
        {
            ProblemType = C + "%" + B;
        }
        if (signs == 5)
        {
            ProblemType = "√" + D;
        }
        if (signs == 6)
        {
            ProblemType = B + "²";
        }



    }

    private void CheckAnswer()
    {
       
        if (signs == 0)
        {
            result = A + B;
        }
        if (signs == 1)
        {
            result = A - B;
        }
        if (signs == 2)
        {
            result = A * B;
        }
        if (signs == 3)
        {
            result = C / B;
        }
        if (signs == 4)
        {
            result = C % B;
        }
        if (signs == 5)
        {
            result = Mathf.Sqrt(D);
        }
        if (signs == 6)
        {
            result = Mathf.Pow(B,2);
        }

        if (Answer == result.ToString())
        {
            score = score + 100;
            genQuestion();
            GameSound.PlayOneShot(buy_02, 0.7f);
        }
        else
        {
            score = score - 100;
            genQuestion();
            GameSound.PlayOneShot(click_02, 0.7f);
        }
    }



    private void GameClock()
    {

        if (Clock > 0)
        {

            Clock -= Time.deltaTime;
        }
        else
        {


            
            stages = Stages.GameOverScreen;
            HighScore();
        }
    }


    private void HighScore()
    {
        if (score > high)
        {
            high = score;
        }

    }
}
