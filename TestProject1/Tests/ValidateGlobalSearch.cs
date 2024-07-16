using TestProject1.Steps;

namespace TestProject1.Tests;

public class ValidateGlobalSearch:BaseTest
{

    [TestCase("Automation")]
    [TestCase("Cloud")]
    [TestCase("BLOCKCHAIN")]
    public void ValidateGlobalSearchTest( string keyWord)
    {
        var globalSearch = new GlobalSearch();
        globalSearch.Search(keyWord);
        logger.Info($"Checking for {keyWord} in all result titles");
        var resultItems =  globalSearch.GetSearchResults(); 
        var itemsWithText = globalSearch.GetSearchResultsWithKeyWord(keyWord);
        Assert.That(resultItems.All(itemsWithText.Contains), $"Total search results: {resultItems.Count}, results containing keyword: {itemsWithText.Count}");
    } 

}