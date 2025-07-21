using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Calc.Models;

namespace Calc.Controllers {
    public class HomeController : Controller {
        public IActionResult Index() {
            return View("Index", Repository.dataRep);
        }

        public ViewResult Submit(CalcData inputData) {
            if (ModelState.IsValid) {
                int x = inputData.X;
                int y = inputData.Y;
                string actionSelect = inputData.Action;

                int answer = Action(x, y, actionSelect);
                Repository.EditAnswer(answer);
                Repository.EditData(inputData);
            }
            return View("Index", Repository.dataRep);
        }

        private int Action(int x, int y, string action) {
            int answerResult = 0;
            switch (action) {
                case "+": answerResult = x + y; break;
                case "-": answerResult = x - y; break;
                case "*": answerResult = x * y; break;
                case "/": answerResult = x / y; break;
            }
            return answerResult;
        }
    }
}
