namespace PIBView
{
    internal static class Program
    {
        public static string ToLoadPib = "";

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(params string[] args)
        {
            if (args.Length > 0)
                if (File.Exists(args[0]))
                    ToLoadPib = args[0];

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}