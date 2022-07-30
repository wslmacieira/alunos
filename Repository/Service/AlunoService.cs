using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using alunos.Model;

namespace alunos.Repository.Service
{
    public class AlunoService
    {
        private readonly HttpClient client = new HttpClient();

        public async Task<List<Aluno>> AdicionaNovosAlunos(int count)
        {
            List<string> alunos = null;
            string path = "https://gerador-nomes.herokuapp.com/nomes/5";
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                alunos = await response.Content.ReadAsAsync<List<string>>();
            }
            List<Aluno> matriculas = new List<Aluno>();
            foreach (var nome in alunos)
            {
                matriculas.Add(
                    new Aluno()
                    {
                        Id = count++,
                        Nome = nome,
                        DataNascimento = RandomDate()
                    }
                );
            }
            return matriculas;
        }

        private static DateTime RandomDate()
        {
            Random random = new Random();
            double dia = random.Next(1, 30);
            int mes = random.Next(1, 12);
            int ano = random.Next(18, 50);
            DateTime date = DateTime.Now.AddDays(dia).AddMonths(mes).AddYears(-ano);
            return date;
        }
    }
}
