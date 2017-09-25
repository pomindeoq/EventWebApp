using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Event.Models;
using Event.Models.Respone.Item;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Event.Models.Categories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Event.Controllers
{
    public class ItemController : Controller
    {
        HttpClient client;
        string url = "http://localhost:54443";

        private readonly UserManager<ApplicationUser> _userManager;

        public ItemController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;

            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }

        // GET: Items
        public async Task<ActionResult> Items()
        {

            HttpResponseMessage Response = await client.GetAsync("api/Item/getItems");

            if (Response.IsSuccessStatusCode)
            {
                var ItemResponse = Response.Content.ReadAsStringAsync().Result;

                var itemsResponse = JsonConvert.DeserializeObject<ItemsResponse>(ItemResponse);

                return View(itemsResponse.Items);

            }

            return View("Error");
        }

        // GET: Item/Details/5
        public async Task<ActionResult> Details(int id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync("api/Item/getAllItems" + "/" + id);

            if (responseMessage.IsSuccessStatusCode)
            {
                var dataResponse = responseMessage.Content.ReadAsStringAsync().Result;

                var Item = JsonConvert.DeserializeObject<Item>(dataResponse);

                return View(Item);
            }

            return View("Error");
        }

        // GET: Item/Create       
        public async Task<ActionResult> Create()
        {
            CreateItemViewModel model = new CreateItemViewModel();

            HttpResponseMessage responseMessage = await client.GetAsync("api/Item/getItemCategories");

            var ItemCategoryResponse = responseMessage.Content.ReadAsStringAsync().Result;

            var itemCategoriesResponse = JsonConvert.DeserializeObject<ItemCategoriesResponse>(ItemCategoryResponse);

            var categoriesList = new List<SelectListItem>();

            foreach (var category in itemCategoriesResponse.ItemCategories)
            {
                categoriesList.Add(new SelectListItem
                {
                    Value = category.Id.ToString(),
                    Text = category.Name
                });
            }

            model.ItemCategories = categoriesList;

            return View(model);
        }

        // POST: Item/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateItemViewModel itemViewModel)
        {
            CreateItemModel itemModel = new CreateItemModel();

            var user = await _userManager.GetUserAsync(HttpContext.User);

            itemModel.CategoryId = Int32.Parse(itemViewModel.SelectedValue);

            itemModel.UserId = user.Id;

            itemModel.PointValue = itemViewModel.PointValue;

            HttpResponseMessage responseMesssage = await client.PostAsJsonAsync("api/Item/createItem", itemModel);

            responseMesssage.EnsureSuccessStatusCode();

            return RedirectToAction("Items");
        }

        // GET: Item/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync("api/Item/getItem/id={id}" + "/" + id);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var Item = JsonConvert.DeserializeObject<Item>(responseData);

                return View(Item);
            }

            return View("Error");

        }

        // POST: Item/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Item item)
        {
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync("api/Item/createItem" + "/" + id, item);

            if (responseMessage.IsSuccessStatusCode)
            {
                RedirectToAction("ItemsList");
            }

            return RedirectToAction("Error");

        }

        // GET: Item/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync("api/Item/DeleteItem" + "/" + id);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var Item = JsonConvert.DeserializeObject<Item>(responseData);

                return View(Item);

            }

            return View("Error");
        }

        // POST: Item/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            HttpResponseMessage responseMessage = await client.DeleteAsync("api / Item / DeleteItem" + "/" + id);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ItemList");
            }

            return View("Error");

        }

        public ActionResult Exchange(int id)
        {
            ItemExchangeViewModel itemExchangeViewModel = new ItemExchangeViewModel();

            itemExchangeViewModel.ItemId = id;

            return View(itemExchangeViewModel);
                        
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Exchange(ItemExchangeViewModel itemExchangeviewModel)
        {
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync("api/Item/exchangeItem", itemExchangeviewModel);

            responseMessage.EnsureSuccessStatusCode();
            
            return  RedirectToAction("Items");             
                       
        }

    }
}