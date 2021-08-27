using OpenQA.Selenium;


public class gitHubPage
{
    private IWebDriver driver;

    public gitHubPage(IWebDriver driver)
    {
        this.driver = driver;
    }

    #region Webelement

    private By searchText = By.XPath("/html/body/div[1]/header/div/div[2]/div[2]/div[1]/div/div/form/label/input[1]");

    private By linuxRepo = By.LinkText("torvalds/linux");

    private By linuxstars = By.CssSelector("#repository-container-header > div.d-flex.mb-3.px-3.px-md-4.px-lg-5 > ul > li:nth-child(2) > a.social-count.js-social-count");

    #endregion

    #region Action    

    public void EnterSearchInformation(string searchInformation)
    {
        driver.FindElement(searchText).SendKeys(searchInformation);
    }
    public void ClickOnLinux()
    {
        driver.FindElement(linuxRepo).Click();
    }
    public string GetStarsCount()
    {
        return driver.FindElement(linuxstars).Text;
    }

    #endregion

    #region Navigation


    #endregion


}
