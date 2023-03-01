using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Assignment2model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Assignment2consume.Controllers
{
    [Route("[controller]")]
    [Route("[controller]/[action]")]
    public class LocationController : Controller
    {

        string Baseurl = "https://localhost:7223";
        [HttpGet]
        public async Task<IActionResult> AddLocation()
        {
            IEnumerable<EmpDetail> emp = null;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                var res = await client.GetAsync("/api/EmpApi/GetData");

                if (res.IsSuccessStatusCode)
                {
                    emp = await res.Content.ReadFromJsonAsync<IList<EmpDetail>>();
                    ViewBag.emp = emp;
                }

            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddLocation(Location loc)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                var res = await client.PostAsJsonAsync("/api/LocationApi/", loc);

                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("LocationList");

                }
            }
            IEnumerable<EmpDetail> emp = null;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                var res = await client.GetAsync("/api/EmpApi/GetData");

                if (res.IsSuccessStatusCode)
                {
                    emp = await res.Content.ReadFromJsonAsync<IList<EmpDetail>>();
                    ViewBag.emp = emp;
                }

            }
            return View();
        }
        public async Task<IActionResult> LocationList()
        {
            IEnumerable<Location> h = null;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                var res = await client.GetAsync("/api/LocationApi/");

                if (res.IsSuccessStatusCode)
                {
                    h = await res.Content.ReadFromJsonAsync<IList<Location>>();
                }
            }

            return View(h);
        }
[HttpGet]
        public async Task<IActionResult> LocationEdit(int id)
        {
            IEnumerable<EmpDetail> emp = null;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                var res = await client.GetAsync("/api/EmpApi/GetData");

                if (res.IsSuccessStatusCode)
                {
                    emp = await res.Content.ReadFromJsonAsync<IList<EmpDetail>>();
                    ViewBag.emp = emp;
                }

            }
            Location l=null;
            using (HttpClient client1 = new HttpClient())
            {
               client1.BaseAddress=new Uri(Baseurl);

               var res1= await client1.GetAsync("/api/LocationApi/"+ id.ToString());

               if(res1.IsSuccessStatusCode)
               {
                 
                    l=await res1.Content.ReadFromJsonAsync<Location>();
               }
                

            }
              return View(l);

        }
        [HttpPost]
public async Task<IActionResult> LocationEdit(Location loc)
{
    using (HttpClient client=new HttpClient())
    {
         client.BaseAddress=new Uri(Baseurl);
         var res=await client.PutAsJsonAsync<Location>("/api/LocationApi/",loc);
         if(res.IsSuccessStatusCode)
         {
              return RedirectToAction("LocationList");

         }


    }
    return View();
}

    public async Task<IActionResult> LocationDelete(int id)
    {
        using (HttpClient client=new HttpClient())
        {
            client.BaseAddress=new Uri(Baseurl);

            var res=await client.DeleteAsync("/api/LocationApi/"+id.ToString());

            if(res.IsSuccessStatusCode)
            {
                return RedirectToAction("LocationList");
            }
        }
        
        return View();
    }

    }
}