//SimpleMathGame()

var A: int;
var B: int;
var Score: int;
var Sings: int;
var result: int;
var Clock: float = 120f;
var ProblemType: string;
var Answer: string = "0";
var Big: GUIStyle;
var GameSound: AudioSource;
var buy_02: AudioClip;
var click_01: AudioClip;
var click_02: AudioClip;
var time_01: AudioClip;




fuction Start()
{
	Big = new GUIStyle();
        Big.fontSize = 36;
        genQuestion();
        GameClock();
        GameSound = GetComponent<AudioSource>();
}

fuction GUI()
{
	 Answer = GUI.TextField(new Rect(250, 250, 160, 30), Answer, Big); 
        GUI.Label(new Rect(200, 100, 160, 160), A + ProblemType + B, Big);
        GUI.Label(new Rect(450, 100, 160, 30), "Score:   " + score.ToString(),Big);
        GUI.Label(new Rect(450, 134, 160, 30), "Timer:   " + Clock.ToString("0.00"), Big);

        if (GUI.Button(new Rect(450, 300, 160, 30), "Restart"))
        {
            Clock = 120.00f;
            score = 0;
            genQuestion();
            GameClock();
            GameSound.PlayOneShot(click_02, 0.7f);
        }

        if (GUI.Button(new Rect(450, 375, 160, 30), "Quit"))
       {
            SceneManager.LoadScene("GameOverScreen");
            GameSound.PlayOneShot(click_02, 0.7f);
        }
            

     
        
        if (GUI.Button(new Rect(250, 300, 160, 30), "Submit"))
        {
            CheckAnswer();
            
        }
      
        if (GUI.Button(new Rect(250, 375, 160, 30), "Next"))
        {
            
            genQuestion();
            GameSound.PlayOneShot(click_01, 0.7f);
        }
}
fuction Update()
{
	GameClock();
}
fuction GenQuestion()
{
	 A = Random.Range(0,100); B = Random.Range(0, 10)+1; signs = Random.Range(0, 5);

        if (signs == 0)
        {
            ProblemType = "+";
        }
        if (signs == 1)
        {
            ProblemType = "-";
        }
        if (signs == 2)
        {
            ProblemType = "*";
        }
        if (signs == 3)
        {
            ProblemType = "/";
        }
        if (signs == 4)
        {
            ProblemType = "%";
        }
}
fuction CheckAnswer()
{
	if (ProblemType == "+")
        {
            result = A + B;
        }
        if (ProblemType == "-")
        {
            result = A - B;
        }
        if (ProblemType == "*")
        {
            result = A * B;
        }
        if (ProblemType == "/")
        {
            result = A / B;
        }
        if (ProblemType == "%")
        {
            result = A % B;
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
fuction GameClock()
{
	if (Clock > 0)
        {

            Clock -= Time.deltaTime;
        }
        else
        {
            GameSound.PlayOneShot(time_01, 1.0f);
            SceneManager.LoadScene("GameOverScreen");

        }
}