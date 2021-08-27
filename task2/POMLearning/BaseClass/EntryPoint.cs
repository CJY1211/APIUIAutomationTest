using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


public class EntryPoint
    {
    public static IWebDriver driver = new ChromeDriver();
 
    public static void Main()
    {
       
    }
    public static void Close() 
    {
        driver.Close();
        driver.Quit();
     }
}