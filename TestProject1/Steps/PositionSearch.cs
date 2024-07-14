using OpenQA.Selenium;
using TestProject1.Pages;

namespace TestProject1.Steps;

public class PositionSearch
{

    readonly CarriersPage carriersPage = new();
    public void Search(string keyWord, string country)
    {
        carriersPage.OpenCareers();
        carriersPage.EnterSearchKeyWord(keyWord);
        carriersPage.EnterLocationValue(country);
        carriersPage.SetRemoteParameter();
        carriersPage.ClickCarriersFindButton();
        carriersPage.OpenLastSearchResult();
    }
    
    public IWebElement GetResultName()
    {
        return carriersPage.GetResultPageName();
    }
}