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
using Event.Models.Categories;

namespace Event.Controllers
{
    public class CategoryController : Controller
    {
        HttpClient client;
        string url = "http://localhost:54443";


        public CategoryController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        // GET: Category
        public async Task<ActionResult> Categories()
        {
            HttpResponseMessage Response = await client.GetAsync("api/Item/getItemCategories");

            if (Response.IsSuccessStatusCode)
            {
                var ItemCategoryResponse = Response.Content.ReadAsStringAsync().Result;

                var itemCategoriesResponse = JsonConvert.DeserializeObject<ItemCategoriesResponse>(ItemCategoryResponse);

                return View(itemCategoriesResponse.ItemCategories);
            }

            return View("Error");
        }

        // GET: Category/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View(new CreateItemCategoryModel());
        }
        
        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateItemCategoryModel itemCategoryModel)
        {
            
            HttpResponseMessage responseMesssage = await client.PostAsJsonAsync("api/Item/createItemCategory", itemCategoryModel);

            responseMesssage.EnsureSuccessStatusCode();
                                                 
            return RedirectToAction("Categories"); 
            
        }
        
        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Categories));
            }
            catch
            {
                return View();
            }
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Category/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Categories));
            }
            catch
            {
                return View();
            }
        }
    }
}