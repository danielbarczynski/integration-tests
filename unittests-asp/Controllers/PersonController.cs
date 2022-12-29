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
    public async Task<IActionResult> GetPerson(int? id)
    {
        var person = await _db.Persons.FirstOrDefaultAsync(x => x.Id == id);
        return View(person);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(PersonModel person)
    {
        if (person == null)
            return NotFound();

        _db.Persons.Add(person);
        await _db.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Update(int? id)
    {
        if (id == null)
            return NotFound();

        var person = await _db.Persons.FirstOrDefaultAsync(x => x.Id == id);
        if (person == null)
            return NotFound();

        return View(person);
    }

    [HttpPost]
    public async Task<IActionResult> Update(PersonModel person)
    {
        _db.Persons.Update(person);
        await _db.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var person = await _db.Persons.FirstOrDefaultAsync(x => x.Id == id);
        if (person == null)
            return NotFound();

        _db.Persons.Remove(person);
        await _db.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}