using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using TestProject1.Core;
using TestProject1.Pages;

namespace SpecFlowProject1.StepDefinitions
{
    [Binding]
    public sealed class InsightsStepDefinitions
    {
        private InsightsPage? insightsPage;
        private IWebDriver driver = BrowserFactory.Driver;
        private string? ArticleNameInCarousel;

        private InsightsPage OnInsightsPage()
        {
            insightsPage ??= new InsightsPage(driver);
            return insightsPage;
        }

        [Given("I swipe first carousel in Insight page (.*) times")]
        public void GivenSwipeFirstCarousel(int counter)
        {
            OnInsightsPage().OpenInsights();
            OnInsightsPage().SwipeFirstCarousel(counter);
        }


        [When("I open selected article from carousel")]
        public string WhenOpenArticleFromCarousel()
        {
            ArticleNameInCarousel = OnInsightsPage().OpenArticleFromFirstCarousel();
            return ArticleNameInCarousel;
        }

        [Then("The selected article has opened")]
        public void ThenTheSelectedArticleHasOpened()
        {
            var ArticleNameOnPage = OnInsightsPage().GetArticleNameOnPage();
            StringAssert.Contains(ArticleNameInCarousel.Remove(ArticleNameInCarousel.Length - 1), ArticleNameOnPage);
        }
    }
}
