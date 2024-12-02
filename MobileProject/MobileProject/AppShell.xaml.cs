namespace MobileProject
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        public void DisableAllTabs()
        {
            // ShellItems는 Tab을 포함하지 않지만 ShellContent를 포함하고 있습니다.
            foreach (var item in this.Items)
            {
                if (item is ShellItem shellItem)
                {
                    foreach (var tab in shellItem.Items)
                    {
                        if (tab is Tab tabItem)
                        {
                            tabItem.IsEnabled = false;
                        }
                    }
                }
            }
        }

        public void EnableAllTabs()
        {
            foreach (var item in this.Items)
            {
                if (item is ShellItem shellItem)
                {
                    foreach (var tab in shellItem.Items)
                    {
                        if (tab is Tab tabItem)
                        {
                            tabItem.IsEnabled = true;
                        }
                    }
                }
            }
        }



    }
}
