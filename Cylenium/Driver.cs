using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Cylenium
{
     public class Driver
     {
         [ThreadStatic]
         public static IWebDriver WebDriver;

         public static void Init()
         {
             WebDriver = new ChromeDriver();
         }

         public static void Visit(string url)
         {
             WebDriver.Url = url;
         }

         public static void Quit()
         {
             WebDriver.Quit();
         }
     }
}
