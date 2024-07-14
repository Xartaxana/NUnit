using TestProject1.Steps;

namespace TestProject1.Tests;

public class ValidatePositionSearch:BaseTest
{

    [TestCase(".NET", "Georgia")]
    [TestCase("Python", "Kazakhstan")]
    [TestCase("JavaScript", "All Locations")]

    [Test]
    public void ValidatePositionSearchTest(string keyWord, string country)
    {
        var positionSearch = new PositionSearch();
        Thread.Sleep(2000);//Without this wait, 2 log files are created
        logger.Info("Test " + TestContext.CurrentContext.Test.Name + " was started");
        positionSearch.Search(keyWord, country);
        var resultPage = positionSearch.GetResultName();
        Assert.That(resultPage.Text, Does.Contain(keyWord)); //Assert is case sensitive
    }

}