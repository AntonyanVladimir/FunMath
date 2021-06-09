using FunMath.Data;
using FunMath.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
            for (int i = 0; i < 5; i++)
            {

                //beim Erstellen des Models geben wir nur den Schwierigkeitsgrad ein
                //und die Aufgabe wird dementsprechend erstellt

                //Schwierigkeitsgrad wird nach jedem 5. Level um Eins erhöht
                var hardnessGrad = levelNr / 5 + 1;
                var challengeModel = new ChallengeModel(hardnessGrad);

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
        private readonly char[] _baseOperatoren = new char[] { '+', '-', '*' };
        public int[] Values { get; set; }
        public int ValueCount { get; set; }
        public int OperatorenCount { get; set; }
        public char[] Operatoren { get; set; }
        public int Minwert { get; set; }
        public int Maxwert { get; set; }
        public string ChallengeText { get; set; }
        public int Result { get; set; }

        public ChallengeModel(int hardnessGrad)
        {
            Minwert = 1;
            Maxwert = hardnessGrad * 20;

            //z.B. bei hardnessGrad = 1 wir haben 3 Zahlen und 2 Operatoren
            ValueCount = hardnessGrad + 2;
            OperatorenCount = ValueCount - 1;

            GetOperatoren(OperatorenCount);
            GetValues(ValueCount);

            for (var i = 0; i < Values.Length - 1; i++)
            {
                ChallengeText += $" {Values[i]} {Operatoren[i]} ";
            }
            ChallengeText += Values[Values.Length - 1];

            Result = GetAnswer();
        }
        private void GetOperatoren(int count)
        {
            Random random = new Random();
            Operatoren = new char[count];
            for (int i = 0; i < count; i++)
            {
                int index = random.Next(_baseOperatoren.Length);
                Operatoren[i] = _baseOperatoren[index];
            }
        }
        private void GetValues(int valueCount)
        {
            Values = new int[valueCount];

            Random random = new Random();
            for (int i = 0; i < valueCount; i++)
            {
                var value = random.Next(this.Minwert, this.Maxwert);
                Values[i] = value;
            }
        }
        private int GetAnswer()
        {
            DataTable dt = new DataTable();

            //aus dem string z.B.: "2+4*3" wird den mathematischen Ausdruck generiert und berechnet.
            int answer = (int)dt.Compute(ChallengeText, "");

            return answer;
        }
    }
}

