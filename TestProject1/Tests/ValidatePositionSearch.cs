using TestProject1.Pages;

namespace TestProject1.Tests;

public class ValidatePositionSearch:BaseTest
{

    [TestCase(".NET", "Georgia")]
    [TestCase("Python", "Kazakhstan")]
    [TestCase("JavaScript", "All Locations")]

    [Test]
    public void ValidatePositionSearchTest(string keyWord, string country)
    {
        var carriersPage = new CarriersPage();
        logger.Info("Test " + TestContext.CurrentContext.Test.Name + " was started");
        carriersPage.OpenCareers();
        carriersPage.EnterSearchKeyWord(keyWord);
        carriersPage.EnterLocationValue(country);
        carriersPage.SetRemoteParameter();
        carriersPage.ClickCarriersFindButton();
        carriersPage.OpenLastSearchResult();
        var resultPage = carriersPage.GetResultPageName();
        Assert.That(resultPage.Text, Does.Contain(keyWord)); //Assert is case sensitive
    }

}