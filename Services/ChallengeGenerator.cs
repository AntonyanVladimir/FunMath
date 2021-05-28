using FunMath.Data;
using FunMath.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FunMath.Services
{
    public class ChallengeGenerator
    {
        public Level GenerateChallengesAndLevel(int levelNr)
        {
            var level = new Level() { LevelNumber = levelNr };
            //Create 10 levels
            for (int i = 0; i < 10; i++)
            {

                var challengeModel = new ChallengeModel(1);

                var challenge = new Challenge()
                {
                    LevelNumber = level.LevelNumber,
                    ChallengeText = $"Was macht {challengeModel.ChallengeText} aus?",
                    Solution = challengeModel.Result.ToString()
                };

                level.Challenges.Add(challenge);
            }
            return level;
        }
    }
    public class ChallengeModel
    {
        private readonly char[] _operatoren = new char[] { '+', '-', '*' };
        public int[] Values { get; set; }
        public char[] Operatoren { get; set; }
        //public int HardnessGrad { get; set; }
        public int Minwert { get; set; }
        public int Maxwert { get; set; }
        public string ChallengeText { get; set; }
        public int Result { get; set; }

        public ChallengeModel(int hardnessGrad)
        {
            switch (hardnessGrad)
            {
                case 1:
                    {
                        Operatoren = new char[] { GetOperator(), GetOperator() };
                        Minwert = 1; Maxwert = 100;
                        Values = new int[] { GetValue(), GetValue(), GetValue() };
                        break;
                    }
                default: throw new ArgumentException("hardness grad kann momentan nur 1 sein.");
            }

            for (var i = 0; i < Values.Length - 1; i++)
            {
                ChallengeText += $" {Values[i]} {Operatoren[i]} ";
            }
            ChallengeText += Values[Values.Length - 1];

            Result = GetAnswer();
        }
        private char GetOperator()
        {
            Random random = new Random();
            int index = random.Next(_operatoren.Length);
            return _operatoren[index];
        }
        private int GetValue()
        {
            Random random = new Random();
            return random.Next(this.Minwert, this.Maxwert);
        }
        private int GetAnswer()
        {
            DataTable dt = new DataTable();

            //aus dem string "2+4*3*4"
            int answer = (int)dt.Compute(ChallengeText, "");

            return answer;
        }
    }
}

