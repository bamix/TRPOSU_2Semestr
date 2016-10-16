using System;

namespace lab10
{
    class Branch2 : IBranch
    {
        public override void Run(double x, double y, MainWindow.function function)
        {
            Result = Math.Pow(function(x) - y, 2) + Math.Cos(y);
            Save();
        }

        public override string ToString()
        {
            return $"x - y > 0; {base.ToString()}";
        }
    }
}
