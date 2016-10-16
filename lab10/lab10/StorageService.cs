namespace lab10
{
    class StorageService
    {
        public static bool RememberMax { get; set; } = false;
        public static bool RememberMin { get; set; } = false;

        public static double? Max {
            get
            {
                return max;
            }
            set
            {
                if (RememberMax && (value > max || max == null))
                {
                    max = value;
                }
            }
        }

        public static double? Min
        {
            get
            {
                return min;
            }
            set
            {
                if (RememberMin && (value < min || min == null))
                {
                    min = value;
                }
            }
        }

        private static double? max;

        private static double? min;
    }
}
