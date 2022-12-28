using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


public class PersonController : Controller
{
    readonly Db _db;
    public PersonController(Db db)
    {
        _db = db;
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var persons = await _db.Persons.ToListAsync();
        return View(persons);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(PersonModel? personModel)
    {
        if (personModel == null)
            return NotFound();

        _db.Persons.Add(personModel);
        await _db.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var personModel = await _db.Persons.FirstOrDefaultAsync(x => x.Id == id);
        if (personModel == null)
            return NotFound();

        _db.Persons.Remove(personModel);
        await _db.SaveChangesAsync();
        return View(personModel);
    }
}