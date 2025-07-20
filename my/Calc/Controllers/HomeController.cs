using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Calc.Models;

namespace Calc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // string viewModel = "42";
            // return View("Index", viewModel);
            // CalcData data = new CalcData();
            // data.Str = viewModel += "!";
            // data.Str += "!";


            return View("Index", Repository.dataRep);
        }

        public ViewResult Submit(CalcData inputData)
        {
            int x = inputData.X;
            int y = inputData.Y;
            int answer = x + y;
            Repository.EditAnswer(answer);

            Repository.EditData(inputData);
            return View("Index", Repository.dataRep);
        }
    }
}
