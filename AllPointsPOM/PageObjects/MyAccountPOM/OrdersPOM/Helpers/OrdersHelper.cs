using CommonHelper;

namespace AllPoints.PageObjects.MyAccountPOM.OrdersPOM.Helpers
{
    public static class OrdersHelper
    {
        public static string ToRGBAColor(string cssColor)
        {
            var color = ColorHelper.HexColorConverter(cssColor);

            return $"rgba({color.R}, {color.G}, {color.B}, 1)";
        }
    }
}