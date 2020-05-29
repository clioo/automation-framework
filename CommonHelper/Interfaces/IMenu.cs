using System.Collections.Generic;

namespace CommonHelper.Interfaces
{
    public interface IMenu
    {
        DomElement MenuContainer { get; set; }
        List<string> GetMenuOptions();
        void ClickOnMenuOption(string option);
    }
}