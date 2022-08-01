using Swashbuckle.AspNetCore.Annotations;

namespace alunos.Model
{
    public class MatriculaTask
    {
        [SwaggerSchema(Description = "Tempo em segundos")]
        public int tempo { get; set; }
        [SwaggerSchema(Description = "Quantidade de matriculas")]
        public int quantidade { get; set; }
    }
}