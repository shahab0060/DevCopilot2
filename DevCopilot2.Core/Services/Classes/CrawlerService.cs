using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.Services.Interfaces;
using DevCopilot2.Domain.DTOs.Crawlers;
using DocumentFormat.OpenXml.Bibliography;
using iText.Layout;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace DevCopilot2.Core.Services.Classes
{
    public class CrawlerService : ICrawlerService
    {
        #region constructor

        private readonly IWebDriver driver;
        public CrawlerService()
        {
            driver = new ChromeDriver();
        }

        #endregion

        public async Task<FilterInstagramPostsDto> FilterInstagramPosts(FilterInstagramPostsDto filter)
        {
            try
            {
                //driver.Navigate().GoToUrl("https://www.instagram.com/accounts/login/");
                driver.Navigate().GoToUrl("https://www.instagram.com");
                Thread.Sleep(5000);
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
                wait.Until(d => d.FindElement(By.Name("username")));
                // Enter username and password
                var usernameField = driver.FindElement(By.Name("username"));
                var passwordField = driver.FindElement(By.Name("password"));
                usernameField.SendKeys("johnmodifie5"); // Replace with your username
                passwordField.SendKeys("Admin1$Admin"); // Replace with your password

                // Click the login button
                var loginButton = driver.FindElement(By.CssSelector("button[type='submit']"));
                loginButton.Click();
                Thread.Sleep(10000);
                //wait.Until(d => d.FindElement(By.CssSelector("nav")));
            }
            catch
            {
                //do nothing and keep living healthy dude:))
            }
            driver.Navigate().GoToUrl(filter.Search);
            var posts = new List<InstagramPostListDto>();
            Thread.Sleep(5000);

            var firstPostElement = driver.FindElement(By
                    .CssSelector("div.x1lliihq.x1n2onr6.xh8yej3.x4gyw5p.xfllauq.xo2y696.x11i5rnm.x2pgyrj a.x1i10hfl.xjbqb8w.x1ejq31n.xd10rxx.x1sy0etr.x17r0tee.x972fbf.xcfux6l.x1qhh985.xm0m39n.x9f619.x1ypdohk.xt0psk2.xe8uvvx.xdj266r.x11i5rnm.xat24cr.x1mh8g0r.xexx8yu.x4uap5.x18d9i69.xkhd6sd.x16tdsg8.x1hl2dhg.xggy1nq.x1a2a7pz._a6hd")
                    );
            firstPostElement.Click();
            Thread.Sleep(500);
            bool hasMorePosts = false;
            InstagramPostListDto? firstPost = GetSingleInstagramPost(out hasMorePosts);
            if (firstPost is not null)
                posts.Add(firstPost);
            while (hasMorePosts && !posts.Any(a => a.Url == driver.Url))
            {
                InstagramPostListDto? post = GetSingleInstagramPost(out hasMorePosts);
                if (post is not null)
                    posts.Add(post);
                if (posts.Count == 1) break;
            }
            driver.Quit();
            return new FilterInstagramPostsDto() { InstagramPosts = posts };
        }


        InstagramPostListDto? GetSingleInstagramPost(out bool hasMorePosts)
        {
            IWebElement? descriptionElement;
            driver.ElementExists("h1._ap3a._aaco._aacu._aacx._aad7._aade", out descriptionElement);
            InstagramPostListDto post = new InstagramPostListDto()
            {
                Description = descriptionElement?.Text ?? "",
                Title = descriptionElement?.Text.GetFirstWords(3) ?? driver.Url,
                Url = driver.Url
            };
            var baseImageElements = driver
    .FindElements(By.CssSelector("div div._aagu._aato div._aagv img"));
            foreach (var imageElement in baseImageElements)
            {
                post.ImageUrls.Add(imageElement.GetAttribute("src")); // Add image URL to the list
            }
            IWebElement? nextElement;
            while (driver
                .ElementExists("button._afxw._al46._al47 div._abm0 span svg", out nextElement))
            {
                nextElement!.Click();
                Thread.Sleep(500);

                var imageElements = driver
                .FindElements(By.CssSelector("div div._aagu._aato div._aagv img"));
                foreach (var imageElement in imageElements)
                {
                    post.ImageUrls.Add(imageElement.GetAttribute("src")); // Add image URL to the list
                }
            }
            post.ImageUrls = post.ImageUrls.Distinct().ToList();
            // IWebElement exitButton = driver.FindElement(
            //   By.CssSelector("div.x160vmok.x10l6tqk.x1eu8d0j.x1vjfegm div.x1i10hfl.x972fbf.xcfux6l.x1qhh985.xm0m39n.x9f619.xe8uvvx.xdj266r.x11i5rnm.xat24cr.x1mh8g0r.x16tdsg8.x1hl2dhg.xggy1nq.x1a2a7pz.x6s0dn4.xjbqb8w.x1ejq31n.xd10rxx.x1sy0etr.x17r0tee.x1ypdohk.x78zum5.xl56j7k.x1y1aw1k.x1sxyh0.xwib8y2.xurb0ha.xcdnw81[role='button']"));

            // Click the button
            //exitButton.Click();
            IWebElement? nextPostBtn;
            hasMorePosts = driver.ElementExists("div._aaqh button._abl-", out nextPostBtn, 1);

            if (hasMorePosts)
                nextPostBtn!.Click();
            Thread.Sleep(500);
            if (!post.ImageUrls.Any())
                return null;
            return post;
        }
    }
}
