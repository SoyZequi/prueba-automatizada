using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;
using System.IO;
using System.Threading;

class Program
{
    static void Main()
    {
        try
        {
            // Configuración para Chrome
            var options = new EdgeOptions();
            var driver = new EdgeDriver(options);

            // Carpeta para screenshots
            string captureFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CaptureFolder");
            if (!Directory.Exists(captureFolder))
            {
                Directory.CreateDirectory(captureFolder);
            }

            // Entra a Chrome
            driver.Navigate().GoToUrl("https://www.bing.com/?FORM=Z9FD1");
            CaptureSs(driver, Path.Combine(captureFolder, "1.png"));

            // Entra al SearchBox
            var googlesearch = driver.FindElement(By.Id("sb_form_q"));
            googlesearch.SendKeys("amazon.com");
            Thread.Sleep(TimeSpan.FromSeconds(2));
            CaptureSs(driver, Path.Combine(captureFolder, "2.png"));

            // Te redirigue a la pagina
            driver.Navigate().GoToUrl("https://www.amazon.com");
            CaptureSs(driver, Path.Combine(captureFolder, "3.png"));

            // Entra al SearchBox de Amazon
            var amazonsearch = driver.FindElement(By.Id("twotabsearchtextbox"));
            amazonsearch.SendKeys("monitor koorui");
            Thread.Sleep(TimeSpan.FromSeconds(2));
            CaptureSs(driver, Path.Combine(captureFolder, "4.png"));

            // Presiona el boton de búsqueda
            var searchbutton = driver.FindElement(By.Id("nav-search-submit-button"));
            ExecuteJavaSclick(driver, searchbutton);
            Thread.Sleep(TimeSpan.FromSeconds(2));

            // Te redirigue a la pagina de la busqueda
            driver.Navigate().GoToUrl("https://www.amazon.com/sspa/click?ie=UTF8&spc=MTo0NzgzNjg1MTcwNzQ4MTAwOjE3MTI3OTMwMzA6c3BfYXRmOjMwMDE2NDgwMTAwOTcwMjo6MDo6&url=%2FKOORUI-Monitor-Computer-Desktop-Mountable%2Fdp%2FB09G6TX7L5%2Fref%3Dsr_1_1_sspa%3Fcrid%3DTJO7ZAV63GB9%26dib%3DeyJ2IjoiMSJ9.4r3KVwiaYHgbDaCmo3MNkOcQJiQHqb4dnQ3ijrzDXAYRaLipPZlLHyyC1ugyHBQ1NB2NCjDQZWBGwvAxJMrsdgwRukAdK11mnIaeEGCLyVB2NoKJ9xX5lEWGx8MLnE27fUhFOWNK1X093Xr470hxsYEOG3MjLAKF4xlTMHZrDPpTAt7OIvV0c1PQjwFyhubSX9SvwNkzd7Bz0ZpeYhWVMn5jaBZnfhv4guLPBjHyuPU.1DiOlRb9sWs4rof1PFarsF2-SIDQua7LeKfCzy6qqoY%26dib_tag%3Dse%26keywords%3Dmonitor%2Bkoorui%26qid%3D1712793030%26sprefix%3Dmonitor%2Bkoorui%252Caps%252C127%26sr%3D8-1-spons%26sp_csd%3Dd2lkZ2V0TmFtZT1zcF9hdGY%26psc%3D1");
            Thread.Sleep(TimeSpan.FromSeconds(3));
            CaptureSs(driver, Path.Combine(captureFolder, "5.png"));

            // Presiona el boton de los reviews
            var reviewbutton = driver.FindElement(By.Id("acrCustomerReviewText"));
            ExecuteJavaSclick(driver, reviewbutton);
            Thread.Sleep(TimeSpan.FromSeconds(5));
            CaptureSs(driver, Path.Combine(captureFolder, "6.png"));

            // Agrega el producto al carrito
            var addcartbutton = driver.FindElement(By.Id("add-to-cart-button"));
            ExecuteJavaSclick(driver, addcartbutton);
            Thread.Sleep(TimeSpan.FromSeconds(4));
            CaptureSs(driver, Path.Combine(captureFolder, "7.png"));

            // Presiona el boton que redirige a la pagina principal
            var homebutton = driver.FindElement(By.Id("nav-logo-sprites"));
            ExecuteJavaSclick(driver, homebutton);
            Thread.Sleep(TimeSpan.FromSeconds(4));

            // Presiona el boton del menu
            var menubutton = driver.FindElement(By.Id("nav-hamburger-menu"));
            ExecuteJavaSclick(driver, menubutton);
            Thread.Sleep(TimeSpan.FromSeconds(3));

            // Redirige a Amazon Music
            driver.Navigate().GoToUrl("https://music.amazon.com/?ref=dm_lnd_nw_listn_fd44f942_nav_em__dm_nav_nw_0_2_2_3");
            CaptureSs(driver, Path.Combine(captureFolder, "8.png"));
            Thread.Sleep(TimeSpan.FromSeconds(5));

            // Crea un informe HTML con las capturas de pantalla
            string report = Path.Combine(captureFolder, "Reporte_de_ss.html");
            GeneraHtmlReporte(captureFolder, report);

            Console.WriteLine($"Informe HTML creado en: {report}");

            Thread.Sleep(TimeSpan.FromSeconds(5));

            // Abre el informe HTML en el navegador predeterminado
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = report,
                UseShellExecute = true
            });
        }
        catch (Exception pro)
        {
            Console.WriteLine($"Error: {pro.Message}");
        }
    }

    // Método para ejecutar clics mediante JavaScript
    static void ExecuteJavaSclick(IWebDriver driver, IWebElement element)
    {
        IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
        js.ExecuteScript("arguments[0].click();", element);
    }

    // Método que captura capturas de pantalla
    static void CaptureSs(IWebDriver driver, string filePath)
    {
        try
        {
            ITakesScreenshot screenshotsave = (ITakesScreenshot)driver;
            Screenshot screenshot = screenshotsave.GetScreenshot();
            screenshot.SaveAsFile(filePath, ScreenshotImageFormat.Png);
        }
        catch (Exception prom)
        {
            Console.WriteLine($"Error al capturar ss en la pantalla: {prom.Message}");
        }
    }

    // Método que genera un informe HTML con las capturas de pantalla
    static void GeneraHtmlReporte(string captureFolder, string reportFilePath)
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(reportFilePath))
            {
                sw.WriteLine("<!DOCTYPE html>");
                sw.WriteLine("<html lang=\"es\">");
                sw.WriteLine("<head>");
                sw.WriteLine("    <meta charset=\"UTF-8\">");
                sw.WriteLine("    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
                sw.WriteLine("    <title>Informe de Capturas</title>");
                sw.WriteLine("    <style>");
                sw.WriteLine("        body { font-family: 'Arial', sans-serif; background-color: #f4f4f4; margin: 20px; }");
                sw.WriteLine("        h1 { color: #333; }");
                sw.WriteLine("        img { max-width: 100%; height: auto; border: 1px solid #ddd; margin-bottom: 10px; }");
                sw.WriteLine("    </style>");
                sw.WriteLine("</head>");
                sw.WriteLine("<body>");
                sw.WriteLine("    <h1>Informe de Capturas:</h1>");

                string[] imageFiles = Directory.GetFiles(captureFolder, "*.png");

                foreach (string imageFile in imageFiles)
                {
                    sw.WriteLine($"    <img src=\"{Path.GetFileName(imageFile)}\" alt=\"Captura de pantalla\">");
                }

                sw.WriteLine("</body>");
                sw.WriteLine("</html>");
            }

            Console.WriteLine($"Informe HTML creado en: {reportFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al generar el informe HTML: {ex.Message}");
        }
    }
}
