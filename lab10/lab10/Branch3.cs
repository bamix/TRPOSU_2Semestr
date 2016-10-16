using System;

namespace lab10
{
    class Branch3 : IBranch
    {
        public override void Run(double x, double y, MainWindow.function function)
        {
            Result = Math.Pow(y - function(x), 2) + Math.Tan(y);
            Save();
        }

        public override string ToString()
        {
            return $"x - y < 0; {base.ToString()}";
        }
    }
}
