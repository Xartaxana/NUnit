using TestProject1.Pages;

namespace TestProject1.Tests;

public class ValidateCarouselArticle:BaseTest
{
    
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(4)]
    [TestCase(5)]
    [Test]
    public void ValidateGlobalSearchTest(int counter)
    {
        logger.Info("Test " + TestContext.CurrentContext.Test.Name + " was started");
        Thread.Sleep(2000);//Without this wait, 2 log files are created
        var insightsPage = new InsightsPage();
        insightsPage.OpenInsights();
        insightsPage.SwipeFirstCarousel(counter);
        var ArticleNameInCarousel = insightsPage.OpenArticleFromFirstCarousel();
        var ArticleNameOnPage = insightsPage.GetArticleNameOnPage(); 
        StringAssert.Contains(ArticleNameInCarousel.Remove(ArticleNameInCarousel.Length - 1), ArticleNameOnPage);
    } 

}