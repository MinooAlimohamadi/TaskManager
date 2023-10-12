using Microsoft.AspNetCore.Mvc;
using TaskManager.Logic.Service.Tasks;
using TaskManager.Logic.Service.Tasks.Dto;

namespace TaskManager.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            this._taskService = taskService;
        }


        public IActionResult TaskList()
        {
            var result = _taskService.GET().ToList();

            return View(result);
        }

        public IActionResult Create()
        {
            var result = new Task_dtod();
            result.DueDate = DateTime.Now;

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Task_dtod view)
        {
            if (ModelState.IsValid)
            {
                var result = await _taskService.POST(view);

                return RedirectToAction("TaskList");
            }

            return View(view);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var result = await _taskService.GET(id);

            return View(result);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Task_dtod view)
        {
            if (ModelState.IsValid)
            {
                var result = await _taskService.PUT(view);

                return RedirectToAction("TaskList");
            }

            return View(view);
        }

        public async Task<ActionResult> Delete(int id)
        {
            var result = await _taskService.DELETE(id);

            return RedirectToAction("TaskList");
        }
    }
}
