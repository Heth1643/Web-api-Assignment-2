using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Assignment2consume.Models;
using Assignment2model;
using Newtonsoft.Json;


namespace Assignment2consume.Controllers;

public class HomeController : Controller
{
    string Baseurl = "https://localhost:7223";
    public async Task<IActionResult> Index()
    {
        return View();


    }

    public async Task<IActionResult> Employee()
    {

        List<Location> emp = new List<Location>();
        using (HttpClient client = new HttpClient())
        {
            client.BaseAddress = new Uri(Baseurl);


            HttpResponseMessage Res = await client.GetAsync("/api/LocationApi/");

            if (Res.IsSuccessStatusCode)
            {

                var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                emp = JsonConvert.DeserializeObject<List<Location>>(EmpResponse);
                ViewBag.Location = emp;
            }




        }
        return View();
    }


    [HttpPost]


    public async Task<IActionResult> Employee(EmpDetail emp)
    {
        using (HttpClient client = new HttpClient())
        {
            client.BaseAddress = new Uri(Baseurl);
            var result = await client.PostAsJsonAsync<EmpDetail>("/api/EmpApi/AddEmp", emp);
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("EmpList");
            }


        }
        List<Location> emp1 = new List<Location>();
        using (HttpClient client = new HttpClient())
        {
            client.BaseAddress = new Uri(Baseurl);


            HttpResponseMessage Res = await client.GetAsync("/api/LocationApi");

            if (Res.IsSuccessStatusCode)
            {

                var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                emp1 = JsonConvert.DeserializeObject<List<Location>>(EmpResponse);
                ViewBag.Location = emp1;
            }




        }

        return View();
    }

    public async Task<IActionResult> EmpList()
    {

        List<EmpDetail> emp = new List<EmpDetail>();
        using (HttpClient client = new HttpClient())
        {
            client.BaseAddress = new Uri(Baseurl);


            HttpResponseMessage Res = await client.GetAsync("/api/EmpApi/GetData");

            if (Res.IsSuccessStatusCode)
            {

                var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                emp = JsonConvert.DeserializeObject<List<EmpDetail>>(EmpResponse);
            }
            return View(emp);
        }

    }

    public async Task<IActionResult> Delete(int id)
    {


        using (HttpClient client = new HttpClient())
        {
            client.BaseAddress = new Uri(Baseurl);
            var result = await client.DeleteAsync("/api/EmpApi/" + id.ToString());
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("EmpList");
            }
        }
        return View();
    }
    public async Task<IActionResult> Edit(int id)
    {
        List<Location> emp1 = new List<Location>();
        using (HttpClient client = new HttpClient())
        {
            client.BaseAddress = new Uri(Baseurl);


            HttpResponseMessage Res = await client.GetAsync("/api/LocationApi/");

            if (Res.IsSuccessStatusCode)
            {

                var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                emp1 = JsonConvert.DeserializeObject<List<Location>>(EmpResponse);
                ViewBag.Location = emp1;
            }
        }

        EmpDetail em = null;
        using (HttpClient client1 = new HttpClient())
        {
            client1.BaseAddress = new Uri(Baseurl);
            var result = await client1.GetAsync("/api/EmpApi/" + id.ToString());
            if (result.IsSuccessStatusCode)
            {
                em = await result.Content.ReadFromJsonAsync<EmpDetail>();
            }
        }
        return View(em);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(EmpDetail Emp)
    {
        using (HttpClient client = new HttpClient())
        {
            client.BaseAddress = new Uri(Baseurl);

            var res = await client.PutAsJsonAsync<EmpDetail>("api/EmpApi/", Emp);

            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("EmpList");
            }

        }

        return View();
    }

}
