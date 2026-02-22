namespace SqlView
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Initialize SQLite native library
            // SQLitePCL.Batteries.Init();

            ApplicationConfiguration.Initialize();
            Utilities.LocalConfigDatabase.Initialize();

            // Show connection selector if no connections exist or user needs to choose
            if (!Utilities.LocalConfigDatabase.HasConnections())
            {
                MessageBox.Show(
                    "No database connections configured.\n\nPlease add a connection to continue.",
                    "First Time Setup",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            Application.Run(new DisplayForm());
        }
    }
}