using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yacht
{
    internal static class YachtGameManager
    {
        public static void SetHints(List<ScoreIndicatingLabel> scores, DiceSet diceSet)
        {
            for(int i = 0; i < Enum.GetValues(typeof(DiceSet.category)).Length; i++)
            {
                if (scores[i].enabled)
                {
                    scores[i].SetHint(diceSet.GetPoint((DiceSet.category)i).ToString());
                }
            }
        }

        public static void SetDiceButtons(DiceSet diceSet, DiceToggleButton[] diceToggles)
        {
            for (int i = 0; i < 5; i++)
                diceToggles[i].SetDice(diceSet.Dices[i]);
        }

        public static void ClearDices(DiceToggleButton[] diceToggles)
        {
            foreach (var d in diceToggles)
                d.ClearDice();
        }

        public static void UpdateScoreBoard(ScoreBoard scoreBoard, Label[] totalLabels)
        {
            totalLabels[0].Text = string.Format("{0} / {1}", scoreBoard.GetSubTotal(), 63);
            totalLabels[1].Text = (scoreBoard.GetBonusEnabled()) ? "35" : "";
            totalLabels[2].Text = scoreBoard.GetTotal().ToString();
        }
    }
}
