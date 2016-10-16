namespace lab10
{
    public abstract class IBranch
    {
        protected double Result { get; set; }
        public abstract void Run(double x, double y, MainWindow.function function);

        protected void Save()
        {
            StorageService.Max = Result;
            StorageService.Min = Result;
        }

        public override string ToString()
        {
            var min = StorageService.RememberMin ? $" min: {StorageService.Min?.ToString()??"-"};" : "";
            var max = StorageService.RememberMax ? $" max: {StorageService.Max?.ToString()??"-"};" : "";
            return $"c = {Result}; {min} {max}";
        }
    }
}
