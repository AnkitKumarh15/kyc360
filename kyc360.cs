// Assignment given by KYC360 :-

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

[Route("api/entities")]
[ApiController]
public class EntityController : ControllerBase
{
    private readonly List<Entity> entities = new List<Entity>();

    [HttpGet]
    public ActionResult<IEnumerable<Entity>> GetEntities([FromQuery] string search)
    {
        if (string.IsNullOrEmpty(search))
        {
            return Ok(entities);
        }

        var searchResults = entities.Where(e =>
            e.Names.Any(n =>
                (n.FirstName != null && n.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                (n.MiddleName != null && n.MiddleName.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                (n.Surname != null && n.Surname.Contains(search, StringComparison.OrdinalIgnoreCase))) ||
            e.Addresses.Any(a =>
                (a.AddressLine != null && a.AddressLine.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                (a.Country != null && a.Country.Contains(search, StringComparison.OrdinalIgnoreCase)))).ToList();

        return Ok(searchResults);
    }

    [HttpGet("{id}")]
    public ActionResult<Entity> GetEntityById(string id)
    {
        var entity = entities.FirstOrDefault(e => e.Id == id);

        if (entity == null)
        {
            return NotFound();
        }

        return Ok(entity);
    }

}
