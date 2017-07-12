using AutoItX3Lib;

namespace addressbook_tests_autoit
{
    public class HelperBase
    {
        protected ApplicationManager manager;
        protected string winTitle;
        protected AutoItX3 autx;

        public HelperBase(ApplicationManager manager)
        {
            this.manager = manager;
            winTitle = ApplicationManager.winTitle;
            this.autx = manager.autx;
        }
    }
}