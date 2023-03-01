using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Assignment2model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Assignment2consume.Controllers
{
    [Route("[controller]")]
    [Route("[controller]/[action]")]
    public class HrController : Controller
    {
        string Baseurl = "https://localhost:7223";

        public async Task<IActionResult> AddHr()
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

        public async Task<IActionResult> AddHr(Hr h)
        {

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                var res = await client.PostAsJsonAsync<Hr>("/api/HrApi/AddHr", h);

                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("HrList");

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

        public async Task<IActionResult> HrList()
        {
            IEnumerable<Hr> h = null;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                var res = await client.GetAsync("/api/HrApi/GetHr");

                if (res.IsSuccessStatusCode)
                {
                    h = await res.Content.ReadFromJsonAsync<IList<Hr>>();

                }

            }

            return View(h);
        }

[HttpGet]    
    public async Task<IActionResult> HrEdit(int id)
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

          Hr h=null;

          using (HttpClient client1=new HttpClient())
          {
             client1.BaseAddress=new Uri(Baseurl);

             var res= await client1.GetAsync("/api/HrApi/"+id.ToString());

             if(res.IsSuccessStatusCode)
             {
    
              h= await res.Content.ReadFromJsonAsync<Hr>();

             }         
       
          }
        return View(h);
        }

[HttpPost]

public async Task<IActionResult> HrEdit(Hr hr)
{
   using (HttpClient client=new HttpClient())
   {
     client.BaseAddress=new Uri(Baseurl);

     var res=await client.PutAsJsonAsync<Hr>("/api/HrApi/",hr);

     if(res.IsSuccessStatusCode)
     {
      return RedirectToAction("HrList");
     }
}
  return View();
}


 public async Task<IActionResult> HrDelete(int id)
    {
        using (HttpClient client=new HttpClient())
        {
            client.BaseAddress=new Uri(Baseurl);

            var res=await client.DeleteAsync("/api/HrApi/"+id.ToString());

            if(res.IsSuccessStatusCode)
            {
                return RedirectToAction("HrList");
            }
        }
        
        return View();
    }
}

}