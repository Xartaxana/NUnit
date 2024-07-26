using TestProject1.Pages;

namespace TestProject1.Tests;

public class ValidateCarouselArticle:BaseTest
{
    
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(4)]
    [TestCase(5)]
    public void ValidateCarouselArticleTest(int counter)
    {
        var insightsPage = new InsightsPage(driver);
        insightsPage.OpenInsights();
        insightsPage.SwipeFirstCarousel(counter);
        var ArticleNameInCarousel = insightsPage.OpenArticleFromFirstCarousel();
        var ArticleNameOnPage = insightsPage.GetArticleNameOnPage(); 
        StringAssert.Contains(ArticleNameInCarousel.Remove(ArticleNameInCarousel.Length - 1), ArticleNameOnPage);
    } 

}