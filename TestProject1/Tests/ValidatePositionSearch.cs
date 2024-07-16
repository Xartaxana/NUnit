using TestProject1.Steps;

namespace TestProject1.Tests;

public class ValidatePositionSearch:BaseTest
{

    [TestCase(".NET", "Georgia")]
    [TestCase("Python", "Kazakhstan")]
    [TestCase("JavaScript", "All Locations")]
    public void ValidatePositionSearchTest(string keyWord, string country)
    {
        var positionSearch = new PositionSearch();
        positionSearch.Search(keyWord, country);
        var resultPage = positionSearch.GetResultName();
        Assert.That(resultPage.Text, Does.Contain(keyWord)); //Assert is case sensitive
    }

}