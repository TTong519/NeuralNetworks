namespace CommonLib
{
    public static class Extensions
    {
        public static double MAE(this List<double> list, List<double> target)
        {
            if(list.Count != target.Count)
                throw new ArgumentException("Both lists must have the same number of elements.");
            double total = 0;
            for(int i = 0; i < list.Count; i++)
            {
                total += Math.Abs(list[i] - target[i]);
            }
            return total / list.Count;
        }
    }
}
