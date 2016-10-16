using System;

namespace lab10
{
    class Branch1 : IBranch
    {
        public override void Run(double x, double y, MainWindow.function function)
        {
            Result = Math.Pow(function(x), 2) + Math.Pow(y, 2) + Math.Sin(y);
            Save();
        }

        public override string ToString()
        {
            return $"x - y = 0; {base.ToString()}";
        }
    }
}
