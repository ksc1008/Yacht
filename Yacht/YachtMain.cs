using System.Security.Policy;

namespace Yacht
{
    public partial class Yacht : Form
    {
        static public bool Interactive = false;
        enum GameState {P1BeforeRoll, P1AfterRoll,P2AfterRoll}

        GameState currentState = GameState.P1BeforeRoll;

        Color p1ScoreBG = Color.LemonChiffon;
        Color p1ScoreFG = Color.Black;
        Color p1ScoreHint = Color.FromArgb(95, 95, 95);
        Color p1ScoreHighlight = Color.PaleGoldenrod;
        Color p1ScoreDisabledBG = Color.DarkKhaki;
        Color p1ScoreDisabledFG = Color.Black; 

        Color diceToggleColor = Color.DarkGoldenrod;
        Color diceColor = Color.White;
        Color diceInactiveColor = Color.LightGray;

        ScoreBoard p1 = new();
        ScoreBoard p2 = new();

        DiceSet p1Dices = new();
        DiceSet p2Dices = new();

        DiceToggleButton[] dices= new DiceToggleButton[5];

        List<ScoreIndicatingLabel> p1Scores = new List<ScoreIndicatingLabel>();
        Label[] totalLabelP1 = new Label[3];
        Label[] totalLabelP2 = new Label[3];

        int remainingReroll = 0;


        public Yacht()
        {
            InitializeComponent();
            Label[] categoryToLabel = { One_P1, Two_P1, Three_P1, Four_P1, Five_P1, Six_P1, Choice_P1, FoK_P1, FullHouse_P1, LStr_P1, BStr_P1, Yacht_P1 };
            dices[0] = new DiceToggleButton(dice1, diceToggleColor, diceColor, diceInactiveColor);
            dices[1] = new DiceToggleButton(dice2, diceToggleColor, diceColor, diceInactiveColor);
            dices[2] = new DiceToggleButton(dice3, diceToggleColor, diceColor, diceInactiveColor);
            dices[3] = new DiceToggleButton(dice4, diceToggleColor, diceColor, diceInactiveColor);
            dices[4] = new DiceToggleButton(dice5, diceToggleColor, diceColor, diceInactiveColor);

            totalLabelP1[0] = SubTotal_P1;
            totalLabelP2[0] = SubTotal_P2;
            totalLabelP1[1] = Bonus_P1;
            totalLabelP2[1] = Bonus_P2;
            totalLabelP1[2] = Total_P1;
            totalLabelP2[2] = Total_P2;

            for (int i = 0; i < Enum.GetValues(typeof(DiceSet.category)).Length; i++)
            {
                int t = i;
                p1Scores.Add(new ScoreIndicatingLabel(p1ScoreFG, p1ScoreHint, p1ScoreBG, p1ScoreHighlight,p1ScoreDisabledBG,p1ScoreDisabledFG));
                p1Scores[p1Scores.Count - 1].SetLabel(categoryToLabel[t], () => {
                    int result = p1.SetScore((DiceSet.category)t, p1Dices.GetPoint((DiceSet.category)t));
                    TurnP2();
                    return result; });
            }

            TurnP1();
        }


        void TurnP1()
        {
            currentState = GameState.P1BeforeRoll;
            rerollButton.Enabled=true;
            YachtGameManager.ClearDices(dices);
        }

        void TurnP2()
        {
            foreach(var dice in dices)
            {
                dice.SetActive(false);
            }
            currentState = GameState.P2AfterRoll;
            Interactive = false;
            rerollButton.Enabled = false;
            YachtGameManager.UpdateScoreBoard(p1, totalLabelP1);

        }

        private void rerollButton_Click(object sender, EventArgs e)
        {
            switch (currentState)
            {
                case GameState.P1BeforeRoll:
                    {
                        p1Dices.Roll(new bool[5] { true, true, true, true, true });
                        for(int i = 0; i < 5; i++)
                        {
                            dices[i].SetActive(true);
                        }
                        currentState = GameState.P1AfterRoll;
                        YachtGameManager.SetDiceButtons(p1Dices, dices);
                        YachtGameManager.SetHints(p1Scores, p1Dices);
                        remainingReroll = 2;
                        Interactive = true;
                        break;
                    }
                case GameState.P1AfterRoll:
                    {
                        p1Dices.Roll(new bool[5] { !dices[0].GetToggled(), !dices[1].GetToggled(), !dices[2].GetToggled(), !dices[3].GetToggled(), !dices[4].GetToggled() });
                        YachtGameManager.SetDiceButtons(p1Dices, dices);
                        YachtGameManager.SetHints(p1Scores, p1Dices);
                        remainingReroll--;
                        if (remainingReroll == 0)
                        {
                            rerollButton.Enabled = false;
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private void debug_toP1_Click(object sender, EventArgs e)
        {
            if (currentState == GameState.P2AfterRoll)
            {
                TurnP1();
            }
        }
    }
}