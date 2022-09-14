using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScraping.Model;
using WebScraping.Helper;
using ScrapySharp.Network;
using ScrapySharp.Extensions;
using System.Diagnostics;
using static Microsoft.FSharp.Core.ByRefKinds;

namespace WebScraping.Services
{
    internal class ScrapingService
    {
        static ScrapingBrowser _scrapingbrowser = new ScrapingBrowser();

        internal List<RowModel> StartElemetPropertyScraping()
        {
            try
            {
                var inputs = GetElementPropertyInputs();

                var uri = new System.Uri(inputs.URL);
                string baseUrl = uri.Scheme + "://" + uri.Host;


                var docs = GetHtmlDocs(inputs.URL);

                var lstResult = new List<RowModel>();
                HtmlAttribute attribute;
                if (string.IsNullOrEmpty(inputs.Property))
                {
                    foreach (var item in docs.DocumentNode.SelectNodes($"//{inputs.Element}"))
                    {
                        attribute = item.Attributes.First();
                        if(!attribute.Value.StartsWith("http") && (inputs.Property == "href" || inputs.Property == "src"))
                            lstResult.Add(new RowModel { Result = baseUrl + attribute.Value });
                        else
                            lstResult.Add(new RowModel { Result = attribute.Value });
                    }
                }
                else
                {
                    foreach (var item in docs.DocumentNode.SelectNodes($"//{inputs.Element}"))
                    {
                        attribute = item.Attributes[$"{inputs.Property}"];
                        if (!attribute.Value.StartsWith("http") && (inputs.Property == "href" || inputs.Property == "src"))
                            lstResult.Add(new RowModel { Result = baseUrl + attribute.Value });
                        else
                            lstResult.Add(new RowModel { Result = attribute.Value });
                    }
                }
                IOHelper.ExportToTXT(lstResult, DateTime.Now.ToFileTime().ToString());
                IOHelper.ExportToCSV(lstResult, DateTime.Now.ToFileTime().ToString());
                IOHelper.ExportToXLSX(lstResult, DateTime.Now.ToFileTime().ToString());
                return lstResult;
            }
            catch (Exception ex)
            {
                return new List<RowModel> { new RowModel { Result = ex.Message } };
            }
        }
        internal List<ProductModel> StartCustomScraping()
        {
            try
            {
                var inputs = GetCustomInputs();
                var lstResult = new List<ProductModel>();
                

                ProductModel productItem;
                var html = GetHtmlNode(inputs.ScrapingURL);
                
                var products = html.CssSelect(inputs.MainNode);

                IEnumerable<HtmlNode>? _product = null,
                                       _oldPrice = null,
                                       _newPrice = null,
                                       _discount = null,
                                       _details = null,
                                       _rate = null,
                                       _user = null,
                                       _mobile = null,
                                       _email = null,
                                       _url = null;

                foreach (var product in products)
                {
                    try
                    {
                        if(!string.IsNullOrEmpty(inputs.ProductNode))
                            _product = product.CssSelect(inputs.ProductNode);
                        
                        if (!string.IsNullOrEmpty(inputs.OldPriceNode))
                            _oldPrice = product.CssSelect(inputs.OldPriceNode);
                        
                        if (!string.IsNullOrEmpty(inputs.NewPriceNode))
                            _newPrice = product.CssSelect(inputs.NewPriceNode);
                        
                        if (!string.IsNullOrEmpty(inputs.DiscountNode))
                            _discount = product.CssSelect(inputs.DiscountNode);
                        
                        if (!string.IsNullOrEmpty(inputs.DetailsNode))
                            _details = product.CssSelect(inputs.DetailsNode);

                        if (!string.IsNullOrEmpty(inputs.RateNode))
                            _rate = product.CssSelect(inputs.RateNode);

                        if (!string.IsNullOrEmpty(inputs.UserNode))
                            _user = product.CssSelect(inputs.UserNode);

                        if (!string.IsNullOrEmpty(inputs.MobileNode))
                            _mobile = product.CssSelect(inputs.MobileNode);

                        if (!string.IsNullOrEmpty(inputs.EmailNode))
                            _email = product.CssSelect(inputs.EmailNode);

                        if (!string.IsNullOrEmpty(inputs.URL))
                            _url = product.CssSelect(inputs.URL);


                        productItem = new ProductModel();

                        if (_product != null && _product.Count() > 0)
                            productItem.Product = _product.Single().InnerHtml;

                        if (_oldPrice != null && _oldPrice.Count() > 0)
                            productItem.OldPrice = _oldPrice.Single().InnerHtml;

                        if (_newPrice != null && _newPrice.Count() > 0)
                            productItem.NewPrice = _newPrice.Single().InnerHtml;

                        if (_discount != null && _discount.Count() > 0)
                            productItem.Discount = _discount.Single().InnerHtml;

                        if (_details != null && _details.Count() > 0)
                            productItem.Details = _details.Single().InnerHtml;

                        if (_rate != null && _rate.Count() > 0)
                            productItem.Rate = _rate.Single().InnerHtml;

                        if (_user != null && _user.Count() > 0)
                            productItem.User = _user.Single().InnerHtml;

                        if (_mobile != null && _mobile.Count() > 0)
                            productItem.Mobile = _mobile.Single().InnerHtml;

                        if (_email != null && _email.Count() > 0)
                            productItem.Email = _email.Single().InnerHtml;

                        if (_url != null && _url.Count() > 0)
                        {
                            if(_url.Single().Attributes.Contains("src"))
                                productItem.URL = _url.Single().GetAttributeValue("src");
                            else if (_url.Single().Attributes.Contains("href"))
                                productItem.URL = _url.Single().GetAttributeValue("href");
                        }
                            

                        lstResult.Add(productItem);
                    }
                    catch
                    {
                        continue;
                    }
                }

                IOHelper.ExportToTXT(lstResult, DateTime.Now.ToFileTime().ToString());
                IOHelper.ExportToCSV(lstResult, DateTime.Now.ToFileTime().ToString());
                IOHelper.ExportToXLSX(lstResult, DateTime.Now.ToFileTime().ToString());
                return lstResult;
            }
            catch (Exception ex)
            {
                return new List<ProductModel> { new ProductModel { Product = ex.Message } };
            }
        }

        
        private HtmlDocument GetHtmlDocs(string url)
        {
            var web = new HtmlWeb();
            return web.Load(url);
        }
        private HtmlNode? GetHtmlNode(string url)
        {
            try
            {

                //_scrapingbrowser.IgnoreCookies = true;
                //_scrapingbrowser.Timeout = TimeSpan.FromMinutes(15);
                //_scrapingbrowser.Headers["User-Agent"] = "";

                WebPage webPage = _scrapingbrowser.NavigateToPage(new Uri(url));
                return webPage.Html;
            }
            catch
            {
                return null;
            }
        }
        
        
        private InputsModel GetElementPropertyInputs()
        {
            InputsModel model = new InputsModel();

            model.URL = IOHelper.GetInput("URL");
            model.Element = IOHelper.GetInput("Element");
            model.Property = IOHelper.GetInput("Property");

            return model;
        }
        private CustomInputsModel GetCustomInputs()
        {
            CustomInputsModel model = new CustomInputsModel();

            model.ScrapingURL = IOHelper.GetInput("Scrapin URL");
            model.MainNode = IOHelper.GetInput("Main Node");
            model.ProductNode = IOHelper.GetInput("Product Node");
            model.OldPriceNode = IOHelper.GetInput("Old Price Node");
            model.NewPriceNode = IOHelper.GetInput("New Price Node");
            model.DiscountNode = IOHelper.GetInput("Discount Node");
            model.DetailsNode = IOHelper.GetInput("Details Node");
            model.RateNode = IOHelper.GetInput("Rate Node");
            model.UserNode = IOHelper.GetInput("User Node");
            model.MobileNode = IOHelper.GetInput("Mobile Node");
            model.EmailNode = IOHelper.GetInput("Email Node");
            model.URL = IOHelper.GetInput("URL");

            return model;
        }

    }
}
