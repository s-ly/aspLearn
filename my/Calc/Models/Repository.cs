namespace Calc.Models
{
    public static class Repository
    {
        public static CalcData dataRep = new CalcData();

        public static void EditData(CalcData data)
        {
            dataRep.Str = data.Str;
            dataRep.X = data.X;
            dataRep.Y = data.Y;
        }

        public static void EditAnswer(int answer)
        {
            dataRep.Answer = answer;
        }
    }
}