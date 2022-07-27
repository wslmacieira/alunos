using System;
using System.Collections.Generic;
using alunos.Model;
using Microsoft.AspNetCore.Mvc;

namespace alunos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        private static List<Aluno> Alunos()
        {
            return new List<Aluno>{
                new Aluno{ Id = 1, Nome = "Wagner", DataNascimento = new DateTime(2000, 12, 5)}
        };
        }

        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            var alunos = Alunos();
            alunos.Add(aluno);
            return Ok(alunos);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Alunos());
        }

    }
}