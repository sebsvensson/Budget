using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer
{
    public static class Colors
    {

        public static bool Theme = true;

        //Light theme
        public static readonly string MenuLight = "#E2E2E2";
        public static readonly string MenuSelectedLight = "#C1C1C1";
        public static readonly string PageLight = "#FFFFFF";
        public static readonly string TextLight = "#383838";
        public static readonly string TextSelectedLight = "#1976C6";

        //Dark theme
        public static readonly string MenuDark = "#21232D";
        public static readonly string MenuSelectedDark = "#181921";
        public static readonly string PageDark = "#2C303D";
        public static readonly string TextDark = "White";
        public static readonly string TextSelectedDark = "White";

        public static string Menu
        {
            get
            {
                if (Theme)
                    return MenuLight;
                else
                    return MenuDark;
            }
        }

        public static string MenuSelected
        {
            get
            {
                if (Theme)
                    return MenuSelectedLight;
                else
                    return MenuSelectedDark;
            }
        }

        public static string Page
        {
            get
            {
                if (Theme)
                    return PageLight;
                else
                    return PageDark;
            }
        }

        public static string Text
        {
            get
            {
                if (Theme)
                    return TextLight;
                else
                    return TextDark;
            }
        }

        public static string TextSelected
        {
            get
            {
                if (Theme)
                    return TextSelectedLight;
                else
                    return TextSelectedDark;
            }
        }
    }
}
