using System;
using BDDExampleDomain;
using Microsoft.AspNetCore.Mvc;

namespace BDDExampleAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QualificationsController : ControllerBase
    {
        private readonly IQualificationsRepository _repo;

        public QualificationsController(IQualificationsRepository repo) => _repo = repo;

        [HttpGet]
        public IActionResult ReadAll()
        {
            var qualifications = new QualificationsHistory
            {
                Qualifications = _repo.ReadAll()
            };

            return Ok(qualifications);
        }

        [HttpGet("{qualificationId}")]
        public IActionResult Read([FromRoute] Guid qualificationId)
        {
            var item = _repo.Read(qualificationId);
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create(Qualification item) => _repo.Create(item) ? Ok() : BadRequest();

        [HttpDelete("{qualificationId}")]
        public IActionResult Delete([FromRoute] Guid qualificationId) => _repo.Delete(qualificationId) ? Ok() : BadRequest();

        [HttpDelete]
        public IActionResult DeleteAll()
        {
            _repo.DeleteAll();
            return Ok();
        }
    }
}