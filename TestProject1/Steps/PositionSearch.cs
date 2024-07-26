using OpenQA.Selenium;
using TestProject1.Pages;

namespace TestProject1.Steps;

public class PositionSearch(IWebDriver driver)
{
    private CarriersPage? carriersPage;
    private IWebDriver driver = driver;

    private CarriersPage OnCarriersPage() {
        carriersPage ??= new CarriersPage(driver);
        return carriersPage;
    }
    public void Search(string keyWord, string country)
    {
        OnCarriersPage().OpenCareers();
        OnCarriersPage().EnterSearchKeyWord(keyWord);
        OnCarriersPage().EnterLocationValue(country);
        OnCarriersPage().SetRemoteParameter();
        OnCarriersPage().ClickCarriersFindButton();
        OnCarriersPage().OpenLastSearchResult();
    }
    
    public IWebElement GetResultName()
    {
        return OnCarriersPage().GetResultPageName();
    }
}