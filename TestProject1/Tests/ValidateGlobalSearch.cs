using log4net;
using log4net.Config;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TestProject1.Pages;
using TestProject1.Core;
using Microsoft.VisualBasic;

namespace TestProject1.Tests;

public class ValidateGlobalSearch:BaseTest
{
    [Test]
    [TestCase("Automation")]
    [TestCase("Cloud")]
    [TestCase("BLOCKCHAIN")]
    public void ValidateGlobalSearchTest( string keyWord)
    {
        var searchPage = new SearchPage();
        logger.Log.Info("Test " + keyWord);
        searchPage.Search(keyWord);
        Thread.Sleep(2000);
        var resultItems =  searchPage.GetResultItems(); 
        var itemsWithText = searchPage.GetResultItemsWithText(keyWord);
        Assert.That(resultItems.All(itemsWithText.Contains));
    } 

}